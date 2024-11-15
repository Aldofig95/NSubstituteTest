using System;
using NSubstitute;

namespace NSubstituteTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.PoweringUp += (sender, e) => Console.WriteLine("Powering up...");
            calculator.PoweringUp += Raise.Event<EventHandler>();

            calculator.Mode = "Scientific";
            Console.WriteLine($"Mode is: {calculator.Mode}");

            calculator.Add(1, 2).Returns(3);
            Console.WriteLine($"Add method returns: {calculator.Add(1, 2)}");

            calculator.Add(1, 2);
            calculator.Received().Add(1, 2);
            calculator.DidNotReceive().Add(5, 7);

            calculator.Mode.Returns("DEC");
            Console.WriteLine($"Mode is: {calculator.Mode}");
            calculator.Mode = "HEX";
            Console.WriteLine($"Mode is: {calculator.Mode}");
        }
    }
}