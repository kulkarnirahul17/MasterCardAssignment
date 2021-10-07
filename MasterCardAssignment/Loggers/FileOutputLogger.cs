using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Loggers
{
    public class FileOutputLogger : IOutputLogger
    {
        private readonly IExceptionLogger _exceptionLogger;
        private const string OUTPUT_FILE_NAME = "output.txt";

        public FileOutputLogger(IExceptionLogger exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
        }

        public void LogOrders(IEnumerable<OrderInfo> orderInfos)
        {
            StreamWriter file = null;
            try
            {
                file = new(OUTPUT_FILE_NAME, append: false);
                file.WriteLine("Order Date Model Price Quantity Sales");
                foreach (var order in orderInfos ?? Enumerable.Empty<OrderInfo>())
                {
                    file.WriteLine(order.ToString());
                }
                file.WriteLine(string.Empty);

            }
            catch (Exception ex)
            {
                _exceptionLogger.LogException(ex);
            }
            finally
            {
                if (file != null)
                    file.Dispose();
            }
        }

        public void LogSalesByModel(Dictionary<string, decimal> salesByModel)
        {
            StreamWriter file = null;
            try
            {
                file = new(OUTPUT_FILE_NAME, append: true);
                //This is the header
                file.WriteLine("Model  Total Sales");
                foreach (var item in salesByModel)
                {
                    file.WriteLine($"{item.Key}  ${item.Value:#,##0.00}");
                }
                file.WriteLine(string.Empty);

            }
            catch (Exception ex)
            {
                _exceptionLogger.LogException(ex);
            }
            finally
            {
                if (file != null)
                    file.Dispose();
            }
        }

        public void LogSalesByYearThenPrice(IEnumerable<OrderInfo> orderInfos)
        {
            StreamWriter file = null;
            try
            {
                file = new(OUTPUT_FILE_NAME, append: true);
                file.WriteLine("Order Date Model Price");
                foreach (var order in orderInfos ?? Enumerable.Empty<OrderInfo>())
                {
                    file.WriteLine($"{order.OrderDate.ToShortDateString()} {order.Model} ${order.Price.ToString("#,##0.00")}");
                }
                file.WriteLine(string.Empty);
            }
            catch (Exception ex)
            {
                _exceptionLogger.LogException(ex);
            }
            finally
            {
                if (file != null)
                    file.Dispose();
            }
        }
    }
}
