using System;
using System.Collections.Generic;
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
        public IEnumerable<OrderInfo> AggregateInputFiles()
        {
            List<OrderInfo> orderInfos = new();

            string pipedFilePath = @"pipe.txt";
            readFile(orderInfos, pipedFilePath, '|');

            string spaceDelimitedFilePath = @"space.dat";
            readFile(orderInfos, spaceDelimitedFilePath, ' ');

            string csvFilePath = @"comma.csv";
            readFile(orderInfos, csvFilePath, ',');                      

            return orderInfos;
        }

        private void readFile(List<OrderInfo> orderInfos, string filePath, char delimiter)
        {
            try
            {              
                orderInfos.AddRange(_reader.ReadInput(filePath, delimiter));
            }
            catch (Exception ex)
            {
                _exceptionLogger.LogException(ex);
            }
        }
    }
}
