using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Loggers
{
    public interface IOutputLogger
    {
        Task LogOrdersAsync(IEnumerable<OrderInfo> orderInfos);
    }
}
