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
        /// <summary>
        /// Combines the results of multiple input files into an Enumerable representation of order info objects
        /// </summary>
        /// <returns>Aggregate list of merge order infos from multiple input sources</returns>
        /// <see cref="OrderInfo"/>
        public IEnumerable<OrderInfo> AggregateInputFiles()
        {
            List<OrderInfo> orderInfos = new();
            OrderInputReader reader = new();            

            string csvFilePath = @"comma.csv";
            orderInfos.AddRange(reader.ReadInput(csvFilePath, ','));
             
            string pipedFilePath = @"pipe.txt";
            orderInfos.AddRange(reader.ReadInput(pipedFilePath, '|'));

            string spaceDelimitedFilePath = @"space.dat";
            orderInfos.AddRange(reader.ReadInput(spaceDelimitedFilePath, ' '));

            return orderInfos;
        }
    }
}
