using System.Collections.Generic;
using MySampleSolution.Models;

namespace MySampleSolution.Business.Comparers
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
