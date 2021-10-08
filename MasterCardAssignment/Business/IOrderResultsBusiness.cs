using System;
using System.Collections.Generic;
using System.Linq;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Business
{
    /// <summary>
    /// Business class which defines methods to group and order order infos
    /// </summary>
    public interface IOrderResultsBusiness
    {
        /// <summary>
        /// Sorts the orders by order date descending
        /// </summary>
        /// <param name="orderInfos">The order info list that needs to be sorted</param>
        /// <returns>Orders sorted by the respective criteria</returns>
        IEnumerable<OrderInfo> SorOrdersByDate(IEnumerable<OrderInfo> orderInfos);        

        /// <summary>
        /// Sorts the orders by top grossing models first
        /// </summary>
        /// <param name="orderInfos">The order info list that needs to be sorted</param>
        /// <returns>Orders sorted by the respective criteria</returns>
        IOrderedEnumerable<KeyValuePair<string, decimal>> GetSalesByModel(IEnumerable<OrderInfo> orderInfos);

        /// <summary>
        /// Sorts the orders sorted by year of order date first and then price descending and then by month descending
        /// </summary>
        /// <param name="orderInfos">The order info list that needs to be sorted</param>
        /// <returns>Orders sorted by the respective criteria</returns>
        IEnumerable<OrderInfo> SortOrdersByYearThenPrice(IEnumerable<OrderInfo> orderInfos);
    }
}
