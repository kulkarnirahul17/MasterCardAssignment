using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterCardAssignment.Loggers;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.FileReaders
{
    /// <summary>
    /// Aggregates the results of reading input from different files
    /// </summary>
    public class ReaderCoordinator : IReaderCoordinator
    {
        private readonly IInputReader _reader;
        private readonly IExceptionLogger _exceptionLogger;

        public ReaderCoordinator(IInputReader reader, IExceptionLogger exceptionLogger)
        {
            _reader = reader;
            _exceptionLogger = exceptionLogger;            
        }

        /// <summary>
        /// Combines the results of multiple input files into an Enumerable representation of order info objects
        /// </summary>
        /// <returns>Aggregate list of merge order infos from multiple input sources</returns>
        /// <see cref="OrderInfo"/>
        public async Task<IEnumerable<OrderInfo>> AggregateInputFilesAsync()
        {
            List<OrderInfo> orderInfos = new();

            string pipedFilePath = @"pipe.txt";
            await readFile(orderInfos, pipedFilePath, '|');

            string spaceDelimitedFilePath = @"space.dat";
            await readFile(orderInfos, spaceDelimitedFilePath, ' ');

            string csvFilePath = @"comma.csv";
            await readFile(orderInfos, csvFilePath, ',');                      

            return orderInfos.AsEnumerable();
        }

        private async Task readFile(List<OrderInfo> orderInfos, string filePath, char delimiter)
        {
            try
            {
                var result = await _reader.ReadInputAsync(filePath, delimiter);
                orderInfos.AddRange(result);
            }
            catch (Exception ex)
            {
                _exceptionLogger.LogException(ex);
            }
        }
    }
}
