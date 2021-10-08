using System.Collections.Generic;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Business.Comparers
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderPriceDescendingComparer : IComparer<OrderInfo>
    {
        public int Compare(OrderInfo x, OrderInfo y)
        {
            return y.Price.CompareTo(x.Price);
        }

    }
}
