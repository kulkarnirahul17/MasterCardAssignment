using System;
using System.Collections.Generic;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Business
{
    public interface IOrderResultsBusiness
    {
        IEnumerable<OrderInfo> SorOrdersByDate(IEnumerable<OrderInfo> orderInfos);
    }
}
