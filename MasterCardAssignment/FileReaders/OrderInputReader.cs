using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MasterCardAssignment.Loggers;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.FileReaders
{
    /// <summary>
    /// Defines the methods for reading the input order from files to parsable order information
    /// </summary>
    public class OrderInputReader : IInputReader
    {
        private readonly IExceptionLogger _exceptionLogger;

        public OrderInputReader(IExceptionLogger exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
        }
        /// <summary>
        /// Defines the methods to read the input orders from a file.
        /// </summary>
        /// <param name="filePath">The file containing input orders</param>
        /// <param name="delimiter">The delimter format of the file</param>
        /// <returns>An enumerable list of orders read from the file represented by <see cref="OrderInfo"/></returns>
        public async Task<IEnumerable<OrderInfo>> ReadInputAsync(string filePath, char delimiter)
        {
            List<OrderInfo> orderInfos = new();

            try
            {
                //Get the full file path of the input file in the executing directory
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

                string line;                
                bool columnHeadersRead = false;

                using (StreamReader file = new(fullPath))
                {
                    while ((line = await file.ReadLineAsync()) != null)
                    {
                        //Other ways of reading are through TextFieldParser where a delimiter can be set
                        //But for this solution, we can skip the headers line
                        if (!columnHeadersRead)
                        {
                            columnHeadersRead = true;
                            continue;
                        }

                        //Get the array of words separated by the delimiter
                        string[] words = line.Split(delimiter);

                        DateTime.TryParse(words[0], out DateTime orderDate);
                        string model = words[1];
                        //Ignore first character because it contains $ symbol
                        decimal.TryParse(words[2][1..], out decimal price);                        
                        int.TryParse(words[3], out int quantity);

                        orderInfos.Add(new OrderInfo(orderDate, model, price, quantity));
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogger.LogException(ex);
                throw;
            }

            return orderInfos;
        }
    }
}
