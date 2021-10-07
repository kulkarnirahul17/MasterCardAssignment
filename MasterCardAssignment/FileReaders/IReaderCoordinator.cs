using System;
using System.Collections.Generic;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.FileReaders
{
    public interface IReaderCoordinator
    {
        IEnumerable<OrderInfo> ReadInput();
    }
}
