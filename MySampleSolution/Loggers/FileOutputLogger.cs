using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MySampleSolution.Models;

namespace MySampleSolution.Loggers
{
    public class FileOutputLogger : IOutputLogger
    {
        private readonly IExceptionLogger _exceptionLogger;
        private const string OUTPUT_FILE_NAME = "output.txt";

        public FileOutputLogger(IExceptionLogger exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
        }

        public async Task LogOrdersAsync(IEnumerable<OrderInfo> orderInfos)
        {
            if (orderInfos is null)
            {
                throw new ArgumentNullException(nameof(orderInfos));
            }

            StreamWriter file = null;
            try
            {
                file = new(OUTPUT_FILE_NAME, append: false);
                file.WriteLine("Order Date Model Price Quantity Sales");
                foreach (var order in orderInfos ?? Enumerable.Empty<OrderInfo>())
                {
                    await file.WriteLineAsync(order.ToString());
                }
                await file.WriteLineAsync(string.Empty);

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

        public async Task LogSalesByModelAsync(IOrderedEnumerable<KeyValuePair<string, decimal>> salesByModel)
        {
            StreamWriter file = null;
            try
            {
                file = new(OUTPUT_FILE_NAME, append: true);
                //This is the header
                file.WriteLine("Model  Total Sales");
                foreach (var item in salesByModel)
                {
                   await file.WriteLineAsync($"{item.Key}  ${item.Value:#,##0.00}");
                }
                await file.WriteLineAsync(string.Empty);

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

        public async Task LogSalesByYearThenPriceAsync(IEnumerable<OrderInfo> orderInfos)
        {
            StreamWriter file = null;
            try
            {
                file = new(OUTPUT_FILE_NAME, append: true);
                file.WriteLine("Order Date Model Price");
                foreach (var order in orderInfos ?? Enumerable.Empty<OrderInfo>())
                {
                    await file.WriteLineAsync($"{order.OrderDate.ToShortDateString()} {order.Model} ${order.Price.ToString("#,##0.00")}");
                }
                await file.WriteLineAsync(string.Empty);
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
