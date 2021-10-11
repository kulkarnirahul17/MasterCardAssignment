using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySampleSolution.Models;

namespace MySampleSolution.FileReaders
{
    /// <summary>
    /// This interface defines the methods to parse input into readable format
    /// </summary>
    public interface IInputReader
    {
        /// <summary>
        /// Defines the methods to read the input orders from a file.
        /// </summary>
        /// <param name="filePath">The file containing input orders</param>
        /// <param name="delimiter">The delimter format of the file</param>
        /// <returns>An enumerable list of orders read from the file represented by <see cref="OrderInfo"/></returns>
        Task<IEnumerable<OrderInfo>> ReadInputAsync(string filePath, char delimiter);
    }
}
