using System;
using System.Collections.Generic;
using MasterCardAssignment.Models;
using System.Linq;

namespace MasterCardAssignment.Business
{
    public class OrderResultsBusiness : IOrderResultsBusiness
    {
        /// <summary>
        /// Sorts the orders by order date descending
        /// </summary>
        /// <param name="orderInfos">The order info list that needs to be sorted</param>
        /// <returns>Orders sorted by the respective criteria</returns>
        public IEnumerable<OrderInfo> SorOrdersByDate(IEnumerable<OrderInfo> orderInfos)
        {
            return orderInfos.OrderByDescending(x => x.OrderDate);
        }

        /// <summary>
        /// Sorts the orders by top grossing models first
        /// </summary>
        /// <param name="orderInfos">The order info list that needs to be sorted</param>
        /// <returns>Orders sorted by the respective criteria</returns>
        public Dictionary<string, decimal> GetSalesByModel(IEnumerable<OrderInfo> orderInfos)
        {
            Dictionary<string, decimal> result = new();
            foreach (var order in orderInfos ?? Enumerable.Empty<OrderInfo>())
            {
                if (result.ContainsKey(order.Model))
                {
                    result[order.Model] += order.Sales;
                }
                else
                {
                    result.Add(order.Model, order.Sales);
                }
            }
            return result;
        }

        /// <summary>
        /// Sorts the orders sorted by year of order date first and then price descending and then by month descending
        /// </summary>
        /// <param name="orderInfos">The order info list that needs to be sorted</param>
        /// <returns>Orders sorted by the respective criteria</returns>

        public IEnumerable<OrderInfo> SortOrdersByYearThenPrice(IEnumerable<OrderInfo> orderInfos)
        {
            return orderInfos.OrderBy(x => x.OrderDate.Year)
                .ThenByDescending(x => x.Price)
                .ThenByDescending(x => x.OrderDate.Month)
                ;
        }
    }
}
