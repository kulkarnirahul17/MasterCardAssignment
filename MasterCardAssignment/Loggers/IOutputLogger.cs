using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Loggers
{
    public interface IOutputLogger
    {
        void LogOrders(IEnumerable<OrderInfo> orderInfos);
        void LogSalesByModel(Dictionary<string, decimal> salesByModel);
        void LogSalesByYearThenPrice(IEnumerable<OrderInfo> orderInfos);
    }
}
