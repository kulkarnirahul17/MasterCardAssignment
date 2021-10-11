using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySampleSolution.Models;

namespace MySampleSolution.FileReaders
{
    /// <summary>
    /// Aggregates the results of reading input from different files
    /// </summary>
    public interface IReaderCoordinator
    {
        /// <summary>
        /// Combines the results of multiple input files into an Enumerable representation of order info objects
        /// </summary>
        /// <returns>Aggregate list of merge order infos from multiple input sources</returns>
        /// <see cref="OrderInfo"/>
        Task<IEnumerable<OrderInfo>> AggregateInputFilesAsync();
    }
}
