using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using MasterCardAssignment.FileReaders;
using MasterCardAssignment.Loggers;
using Moq;
using Shouldly;
using Xunit;

namespace MasterCardAssignmentTests
{
    public class OrderInputReaderTests
    {
        private OrderInputReader _reader;
        private Mock<IExceptionLogger> _exceptionLoggerMock;
        private readonly Fixture _fixture;

        public OrderInputReaderTests()
        {
            _fixture = new Fixture();
            _exceptionLoggerMock = new Mock<IExceptionLogger>(); 
            _reader = new OrderInputReader(_exceptionLoggerMock.Object);
        }

        [Fact]
        public async Task ItShouldThrowExceptionIfFileDoesNotExist()
        {            
            var fileName = _fixture.Create<string>();
            var exception = await Assert.ThrowsAsync<FileNotFoundException>(() => _reader.ReadInputAsync(fileName, ';'));
            exception.Message.ShouldContain(fileName);         
        }

        [Fact]
        public void ItShouldLogException()
        {
            var fileName = _fixture.Create<string>();
            Assert.ThrowsAsync<FileNotFoundException>(() => _reader.ReadInputAsync(fileName, ';'));
            _exceptionLoggerMock.Verify(x => x.LogException(It.IsAny<FileNotFoundException>()), Times.Once);
        }

        [Fact]
        [Trait("Category", "Integration Tests")]
        public async Task ItShouldReadOrdersSuccessfully()
        {
            var fileName = _fixture.Create<string>();
            var result = await _reader.ReadInputAsync("comma.csv", ',');
            result.ShouldNotBeNull();
            result.Count().ShouldBe(4);
        }
    }
}
