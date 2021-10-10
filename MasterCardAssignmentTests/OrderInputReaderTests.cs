using System;
using System.IO;
using System.Linq;
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
        public void ItShouldThrowExceptionIfFileDoesNotExist()
        {            
            var fileName = _fixture.Create<string>();
            var exception = Assert.Throws<FileNotFoundException>(() => _reader.ReadInput(fileName, ';'));
            exception.Message.ShouldContain(fileName);         
        }

        [Fact]
        public void ItShouldLogException()
        {
            var fileName = _fixture.Create<string>();
            Assert.Throws<FileNotFoundException>(() => _reader.ReadInput(fileName, ';'));
            _exceptionLoggerMock.Verify(x => x.LogException(It.IsAny<FileNotFoundException>()), Times.Once);
        }

        [Fact]
        [Trait("Category", "Integration Tests")]
        public void ItShouldReadOrdersSuccessfully()
        {
            var fileName = _fixture.Create<string>();
            var result = _reader.ReadInput("comma.csv", ',');
            result.ShouldNotBeNull();
            result.Count().ShouldBe(4);
        }
    }
}
