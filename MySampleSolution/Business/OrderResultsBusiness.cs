﻿using System;
using System.Collections.Generic;
using MySampleSolution.Models;
using System.Linq;
using MySampleSolution.Business.Comparers;

namespace MySampleSolution.Business
{
    /// <summary>
    /// Business class of Order info objects which orders and sort them by specific criteria
    /// </summary>
    public class OrderResultsBusiness : IOrderResultsBusiness
    {
        /// <summary>
        /// Sorts the orders by order date descending
        /// </summary>
        /// <param name="orderInfos">The order info list that needs to be sorted</param>
        /// <returns>Orders sorted by the respective criteria</returns>
        public IEnumerable<OrderInfo> SorOrdersByDate(IEnumerable<OrderInfo> orderInfos)
        {
            if (orderInfos == null)
                throw new ArgumentNullException(nameof(orderInfos));

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
            if (orderInfos == null)
                throw new ArgumentNullException(nameof(orderInfos));

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
            if (orderInfos == null)
                throw new ArgumentNullException(nameof(orderInfos));

            //Using ThenBy and not ThenByDescending becuase the comparer already compares in decreasing order
            return orderInfos.OrderBy(x => x, new OrderYearComparer())
                .ThenBy(x => x, new OrderPriceDescendingComparer())
                ;
        }        
       
    }
}
