using System;
using System.Collections.Generic;
using AutoFixture;
using MasterCardAssignment.Loggers;
using MasterCardAssignment.Models;
using Moq;
using Xunit;

namespace MasterCardAssignmentTests
{
    public class FileOutputLoggerTests
    {
        private FileOutputLogger _logger;
        private Mock<IExceptionLogger> _exceptionLoggerMock;
        private IEnumerable<OrderInfo> _orderInfos;
        private Fixture _fixture;

        public FileOutputLoggerTests()
        {
            _fixture = new Fixture();
            _orderInfos = _fixture.Create<List<OrderInfo>>();
            _exceptionLoggerMock = new Mock<IExceptionLogger>();
            _logger = new FileOutputLogger(_exceptionLoggerMock.Object);
        }

        [Fact]
        public void ShouldOrderShouldThrowArgumentNullExceptionWhenOrderInfoIsNull()
        {
            _orderInfos = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => _logger.LogOrdersAsync(null));
        }    
    }
}
