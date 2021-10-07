using System;
using System.Collections.Generic;
using MasterCardAssignment.Models;
using System.Linq;

namespace MasterCardAssignment.Business
{
    public class OrderResultsBusiness : IOrderResultsBusiness
    {
        public IEnumerable<OrderInfo> SorOrdersByDate(IEnumerable<OrderInfo> orderInfos)
        {
            return orderInfos.OrderByDescending(x => x.OrderDate);
        }

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

        public IEnumerable<OrderInfo> SortOrdersByYearThenPrice(IEnumerable<OrderInfo> orderInfos)
        {
            return orderInfos.OrderBy(x => x.OrderDate.Year)
                .ThenByDescending(x => x.Price)
                .ThenByDescending(x => x.OrderDate.Month)
                ;
        }
    }
}
