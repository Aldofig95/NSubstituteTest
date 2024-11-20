using NSubstitute;
using NSubstituteTest;
using Xunit;

namespace NSubstituteTest.Tests
{
    public class MockTest
    {
        [Fact]
        public void GetMockInfo()
        {
            var mockService = Substitute.For<IMockService>();
            mockService.GetData().Returns("Mocked Data test");
            var controller = new MockController(mockService);
            var result = controller.GetInfo();

            Assert.Equal("Mocked Data test", result);
        }
    }
}