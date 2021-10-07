using System;
using MasterCardAssignment.Business;
using MasterCardAssignment.FileReaders;
using MasterCardAssignment.Loggers;

namespace MasterCardAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            IExceptionLogger exceptionLogger = new ConsoleExceptionLogger();
            //Read the input
            IReaderCoordinator readerCoordinator = new ReaderCoordinator();
            var orderInfos = readerCoordinator.ReadInput();
            IOrderResultsBusiness business = new OrderResultsBusiness();
            var sortedByDate = business.SorOrdersByDate(orderInfos);

            //Sort by Order Date
            IOutputLogger logger = new FileOutputLogger(exceptionLogger);
            logger.LogOrdersAsync(sortedByDate);
            
        }
    }
}
