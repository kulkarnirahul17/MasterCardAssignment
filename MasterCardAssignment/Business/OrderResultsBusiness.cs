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
            return orderInfos.OrderBy(x => x.OrderDate);
        }
    }
}
