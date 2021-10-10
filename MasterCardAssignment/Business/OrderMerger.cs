using System;
using MasterCardAssignment.FileReaders;
using MasterCardAssignment.Loggers;

namespace MasterCardAssignment.Business
{
    /// <summary>
    /// Responsible for coordination of reading the input files and logging the respective orders
    /// </summary>
    public class OrderMerger : IOrderMerger
    {
        private readonly IReaderCoordinator _readerCoordinator;
        private readonly IOutputLogger _logger;
        private readonly IOrderResultsBusiness _business;

        public OrderMerger(IReaderCoordinator readerCoordinator, IOutputLogger logger, IOrderResultsBusiness business)
        {
            _readerCoordinator = readerCoordinator;
            _logger = logger;
            _business = business;
        }

        /// <summary>
        /// Reads the orders from the input data sources and logs the merged results to output 
        /// </summary>
        public void ReadAndMergeOrders()
        {
            //Read the input
            var orderInfos = _readerCoordinator.AggregateInputFiles();
            
            //Sort by Order Date            
            var sortedByDate = _business.SorOrdersByDate(orderInfos);           
            _logger.LogOrders(sortedByDate);

            //Get sales by top grossing models
            var salesByModel = _business.GetSalesByModel(orderInfos);
            _logger.LogSalesByModel(salesByModel);

            //Log sales by year asc then price desc
            var salesByYearAscThenPriceDesc = _business.SortOrdersByYearThenPrice(orderInfos);
            _logger.LogSalesByYearThenPrice(salesByYearAscThenPriceDesc);
        }       
    }
}
