using System;
using MasterCardAssignment.FileReaders;
using Moq;
using Shouldly;
using System.Collections.Generic;
using MasterCardAssignment.Models;
using AutoFixture;
using Xunit;
using System.Linq;
using MasterCardAssignment.Loggers;
using System.Threading.Tasks;

namespace MasterCardAssignmentTests
{
    public class ReaderCoordinatorTests
    {
        private readonly ReaderCoordinator _coordinator;
        private readonly Mock<IInputReader> _inputReaderMock;
        private readonly Mock<IExceptionLogger> _exceptionLoggerMock;
        private readonly Fixture _fixture;
        
        private List<OrderInfo> _orderInfosPipe;
        private List<OrderInfo> _orderInfosCsv;
        private List<OrderInfo> _orderInfosDat;

        public ReaderCoordinatorTests()
        {
            _fixture = new Fixture();
            _orderInfosPipe = new List<OrderInfo>() { _fixture.Create<OrderInfo>() };
            _orderInfosCsv = new List<OrderInfo>() { _fixture.Create<OrderInfo>() };
            _orderInfosDat = new List<OrderInfo>() { _fixture.Create<OrderInfo>() };

            _inputReaderMock = new Mock<IInputReader>();
            _exceptionLoggerMock = new Mock<IExceptionLogger>();

            _inputReaderMock.Setup(x => x.ReadInputAsync(It.IsAny<string>(), '|')).ReturnsAsync(_orderInfosPipe);
            _inputReaderMock.Setup(x => x.ReadInputAsync(It.IsAny<string>(), ',')).ReturnsAsync(_orderInfosCsv);
            _inputReaderMock.Setup(x => x.ReadInputAsync(It.IsAny<string>(), ' ')).ReturnsAsync(_orderInfosDat);

            _coordinator = new ReaderCoordinator(_inputReaderMock.Object, _exceptionLoggerMock.Object);
        }

        [Fact]
        public async Task ItShouldReadCombinedInputs()
        {
            var result = await _coordinator.AggregateInputFilesAsync();
            result.ShouldContain(_orderInfosCsv.First());
            result.ShouldContain(_orderInfosPipe.First());
            result.ShouldContain(_orderInfosDat.First());
        }

        [Fact]
        public async Task ItShouldSkipCsvIfReadingFails()
        {
            _inputReaderMock.Setup(x => x.ReadInputAsync(It.IsAny<string>(), ',')).Throws<Exception>();

            var result = await _coordinator.AggregateInputFilesAsync();
            result.Count().ShouldBe(2);
            result.ShouldNotContain(_orderInfosCsv.First());
            result.ShouldContain(_orderInfosPipe.First());
            result.ShouldContain(_orderInfosDat.First());
        }

        [Fact]
        public async Task ItShouldLogExceptionIfAnyFileReadingFails()
        {
            var exMessage = _fixture.Create<string>();
            _inputReaderMock.Setup(x => x.ReadInputAsync(It.IsAny<string>(), '|')).Throws(new Exception(exMessage));            
            await _coordinator.AggregateInputFilesAsync();
            _exceptionLoggerMock.Verify(x => x.LogException(It.Is<Exception>(ex => ex.Message == exMessage)), Times.Once);
        }       
    }
}
