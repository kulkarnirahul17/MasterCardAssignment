using System;
using System.Collections.Generic;
using MasterCardAssignment.Models;
using System.Linq;
using MasterCardAssignment.Business.Comparers;

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
            var itemsToSort = orderInfos.ToArray();
            OrderSorter.QuickSort(itemsToSort, new OrderDateReverseComparer());
            return itemsToSort.AsEnumerable();
        }

        /// <summary>
        /// Sorts the orders by top grossing models first
        /// </summary>
        /// <param name="orderInfos">The order info list that needs to be sorted</param>
        /// <returns>Orders sorted by the respective criteria</returns>
        public IOrderedEnumerable<KeyValuePair<string, decimal>> GetSalesByModel(IEnumerable<OrderInfo> orderInfos)
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

            return result.OrderBy(x => x, new OrderModelSalesPairComparator());
        }

        /// <summary>
        /// Sorts the orders sorted by year of order date first and then price descending and
        /// </summary>
        /// <param name="orderInfos">The order info list that needs to be sorted</param>
        /// <returns>Orders sorted by the respective criteria</returns>

        public IEnumerable<OrderInfo> SortOrdersByYearThenPrice(IEnumerable<OrderInfo> orderInfos)
        {
            //Using ThenBy and not ThenByDescending becuase the comparer already compares in decreasing order
            return orderInfos.OrderBy(x => x, new OrderYearComparer())
                .ThenBy(x => x, new OrderPriceDescendingComparer())
                ;
        }        
       
    }
}
