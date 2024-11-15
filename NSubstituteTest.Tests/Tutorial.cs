using System;
using NSubstitute;
using Xunit;

namespace NSubstituteTest.Tests
{

    public class CalculatorTests
    {
        [Fact]
        public void Add_ReturnsExpectedResult()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);
            var result = calculator.Add(1, 2);

            Assert.Equal(3, result);

            calculator.Received().Add(1, 2);
            calculator.DidNotReceive().Add(5, 7);

            calculator.Mode.Returns("DEC");
            Assert.Equal("DEC", calculator.Mode);
            calculator.Mode = "HEX";
            Assert.Equal("HEX", calculator.Mode);
        }
    }
}
