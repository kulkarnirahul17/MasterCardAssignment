using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using MasterCardAssignment.Business;
using MasterCardAssignment.FileReaders;
using MasterCardAssignment.Loggers;
using MasterCardAssignment.Models;
using Moq;
using Xunit;

namespace MasterCardAssignmentTests
{
    public class OrderMergerTests
    {
        private readonly OrderMerger _orderMerger;
        private readonly Mock<IReaderCoordinator> _readerCoordinatorMock;
        private readonly Mock<IOutputLogger> _outputLoggerMock;
        private readonly Mock<IOrderResultsBusiness> _orderResultsBusinessMock;
        private IOrderedEnumerable<KeyValuePair<string, decimal>> _topGrossingByModelOrders;
        private Fixture _fixture;
        private IEnumerable<OrderInfo> _orderInfos;
        private IEnumerable<OrderInfo> _sortedByDateDescOrders;

        public OrderMergerTests()
        {
            _outputLoggerMock = new Mock<IOutputLogger>();          
            _fixture = new Fixture();
            _orderInfos = _fixture.Create<List<OrderInfo>>();
            var result = _fixture.Create<Dictionary<string, decimal>>();
            _topGrossingByModelOrders = result.OrderBy(x => x.Value);
             _sortedByDateDescOrders = _orderInfos.OrderByDescending(y => y.OrderDate.Year);

            _readerCoordinatorMock = new Mock<IReaderCoordinator>();
            _readerCoordinatorMock.Setup(x => x.AggregateInputFilesAsync()).ReturnsAsync(_orderInfos);            

            _orderResultsBusinessMock = new Mock<IOrderResultsBusiness>();
            _orderResultsBusinessMock.Setup(x => x.SorOrdersByDate(_orderInfos)).Returns(_sortedByDateDescOrders);
            _orderResultsBusinessMock.Setup(x => x.GetSalesByModel(_orderInfos)).Returns(_topGrossingByModelOrders);
            _orderResultsBusinessMock.Setup(x => x.SortOrdersByYearThenPrice(_orderInfos)).Returns(_sortedByDateDescOrders);

            _orderMerger = new OrderMerger(_readerCoordinatorMock.Object, _outputLoggerMock.Object, _orderResultsBusinessMock.Object);

        }

        [Fact]
        public async Task ItShouldCallLogOrdersByDate()
        {
            await _orderMerger.ReadAndMergeOrdersAsync();
            _outputLoggerMock.Verify(x => x.LogOrdersAsync(_sortedByDateDescOrders), Times.Once);
        }

        [Fact]
        public async Task ItShouldCallLogSalesByModel()
        {
            await _orderMerger.ReadAndMergeOrdersAsync();
            _outputLoggerMock.Verify(x => x.LogSalesByModelAsync(_topGrossingByModelOrders), Times.Once);
        }

        [Fact]
        public async Task ItShouldCallLogSalesByYearThenPrice()
        {
            await _orderMerger.ReadAndMergeOrdersAsync();
            _outputLoggerMock.Verify(x => x.LogSalesByYearThenPriceAsync(_sortedByDateDescOrders), Times.Once);
        }
    }
}
