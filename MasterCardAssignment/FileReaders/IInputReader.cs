using System;
using System.Collections.Generic;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.FileReaders
{
    public interface IInputReader
    {
        IEnumerable<OrderInfo> ReadInput(string filePath, char delimiter);
    }
}
