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
            var orderInfos = readerCoordinator.AggregateInputFiles();
            IOrderResultsBusiness business = new OrderResultsBusiness();
            var sortedByDate = business.CustomSortedOrdersByDate(orderInfos);

            //Sort by Order Date
            IOutputLogger logger = new FileOutputLogger(exceptionLogger);
            logger.LogOrders(sortedByDate);

            //Get sales by top grossing models
            var salesByModel = business.GetSalesByModel(orderInfos);
            logger.LogSalesByModel(salesByModel);

            //Log sales by year asc then price desc
            var salesByYearAscThenPriceDesc = business.SortOrdersByYearThenPrice(orderInfos);
            logger.LogSalesByYearThenPrice(salesByYearAscThenPriceDesc);
            
        }
    }
}
