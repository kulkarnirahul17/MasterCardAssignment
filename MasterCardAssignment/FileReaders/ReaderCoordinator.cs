using System;
using System.Collections.Generic;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.FileReaders
{
    public class ReaderCoordinator : IReaderCoordinator
    {
        public IEnumerable<OrderInfo> ReadInput()
        {
            List<OrderInfo> orderInfos = new();
            OrderInputReader reader = new();            

            string csvFilePath = "Resources/comma.csv";
            orderInfos.AddRange(reader.ReadInput(csvFilePath, ','));
             
            string pipedFilePath = "Resources/pipe.txt";
            orderInfos.AddRange(reader.ReadInput(pipedFilePath, '|'));

            return orderInfos;
        }
    }
}
