using System;
using System.Collections.Generic;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.FileReaders
{
    /// <summary>
    /// Aggregates the results of reading input from different files
    /// </summary>
    public class ReaderCoordinator : IReaderCoordinator
    {
        private readonly IInputReader _reader;

        public ReaderCoordinator(IInputReader reader)
        {
            _reader = reader;
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
            orderInfos.AddRange(_reader.ReadInput(pipedFilePath, '|'));

            string csvFilePath = @"comma.csv";
            orderInfos.AddRange(_reader.ReadInput(csvFilePath, ','));                         

            string spaceDelimitedFilePath = @"space.dat";
            orderInfos.AddRange(_reader.ReadInput(spaceDelimitedFilePath, ' '));

            return orderInfos;
        }
    }
}
