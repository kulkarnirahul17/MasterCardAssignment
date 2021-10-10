using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Loggers
{
    /// <summary>
    /// Defines the interface for logging orders in their respective sort orders
    /// </summary>
    public interface IOutputLogger
    {
        /// <summary>
        /// Logs the orders in that are sorted in descending order of dates
        /// </summary>
        /// <param name="orderInfos">The sorted order infos that need to be logged</param>
        Task LogOrdersAsync(IEnumerable<OrderInfo> orderInfos);

        /// <summary>
        /// Logs the orders for top selling sales by model
        /// </summary>
        /// <param name="salesByModel">Dictionary of top grossing orders by model</param>
        Task LogSalesByModelAsync(IOrderedEnumerable<KeyValuePair<string, decimal>> salesByModel);

        /// <summary>
        /// Logs the orders sorted by year of order date first and then price descending and then by month descending
        /// </summary>
        /// <param name="orderInfos">The sorted order infos that need to be logged</param>
        Task LogSalesByYearThenPriceAsync(IEnumerable<OrderInfo> orderInfos);
    }
}
