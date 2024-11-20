using System;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace NSubstituteTest.Tests
{
    public class MockTest
    {
        [Fact]
        public void GetInfo_ShouldReturnDataAndLogResult()
        {
            var mockService = Substitute.For<IMockService>();
            var mockLoggingService = Substitute.For<ILoggingService>();
            mockService.GetData().Returns("Mocked Data test");
            var controller = new MockController(mockService, mockLoggingService);
            var result = controller.GetInfo();

            Assert.Equal("Mocked Data test", result);
            mockLoggingService.Received().Log("Data retrieved: Mocked Data test");
        }

        [Fact]
        public void GetInfo_WithException()
        {
            var mockService = Substitute.For<IMockService>();
            var mockLoggingService = Substitute.For<ILoggingService>();
            mockService.GetData().Throws(new Exception("Test Exception"));
            var controller = new MockController(mockService, mockLoggingService);

            var ex = Assert.Throws<Exception>(() => controller.GetInfo());
            Assert.Equal("Test Exception", ex.Message);
            mockLoggingService.Received().Log("Error: Test Exception");
        }
    }
}