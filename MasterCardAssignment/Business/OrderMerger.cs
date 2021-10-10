using System;
using System.Threading.Tasks;
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
        public async Task ReadAndMergeOrdersAsync()
        {
            //Read the input
            var orderInfos = await _readerCoordinator.AggregateInputFilesAsync();
            
            //Sort by Order Date            
            var sortedByDate = _business.SorOrdersByDate(orderInfos);           
            await _logger.LogOrdersAsync(sortedByDate);

            //Get sales by top grossing models
            var salesByModel = _business.GetSalesByModel(orderInfos);
            await _logger.LogSalesByModelAsync(salesByModel);

            //Log sales by year asc then price desc
            var salesByYearAscThenPriceDesc = _business.SortOrdersByYearThenPrice(orderInfos);
            await _logger.LogSalesByYearThenPriceAsync(salesByYearAscThenPriceDesc);
        }       
    }
}
