using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Loggers
{
    public class FileOutputLogger : IOutputLogger
    {
        private readonly IExceptionLogger _exceptionLogger;

        public FileOutputLogger(IExceptionLogger exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
        }

        public async Task LogOrdersAsync (IEnumerable<OrderInfo> orderInfos)
        {
            try
            {
                StreamWriter file = new("output.txt", append: true);
                await file.WriteLineAsync("Order Date Model Price Quantity Sales");

            }
            catch (Exception ex)
            {
                _exceptionLogger.LogException(ex);
            }
        }
    }
}
