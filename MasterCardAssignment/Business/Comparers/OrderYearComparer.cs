using System.Collections.Generic;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Business.Comparers
{
    public class OrderYearComparer : IComparer<OrderInfo>
    {       
        public int Compare(OrderInfo x, OrderInfo y)
        {
            return x.OrderDate.Year.CompareTo(y.OrderDate.Year);
        }
    }
}
