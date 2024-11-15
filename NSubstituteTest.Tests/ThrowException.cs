using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteTest.Tests
{
    public class ExceptionTests
    {
        [Fact]
        public void DoWork_ShouldThrowInvalidOperationException()
        {
            var myService = Substitute.For<IMyService>();
            myService.When(x => x.DoWork()).Do(x => { throw new InvalidOperationException("Error happened !!"); });

            var exception = Assert.Throws<InvalidOperationException>(() => myService.DoWork());
            //If msj is different, we'll get the results
            Assert.Equal("Error happened !!", exception.Message);
        }
    }
}
