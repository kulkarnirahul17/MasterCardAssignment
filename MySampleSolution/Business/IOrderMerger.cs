using System;
using System.Threading.Tasks;

namespace MySampleSolution.Business
{
    /// <summary>
    /// Defines the methods for reading and merging the orders
    /// </summary>
    public interface IOrderMerger
    {
        /// <summary>
        /// Reads the orders from the input data sources and logs the merged results to output 
        /// </summary>
        Task ReadAndMergeOrdersAsync();
    }
}
