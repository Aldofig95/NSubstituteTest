using System;

namespace NSubstituteTest
{
    public interface IBank
    {
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        decimal Balance { get; set; }
        event EventHandler<BalanceChanged> BalanceChanged;
    }

    public class BalanceChanged : EventArgs
    {
        public decimal OldBalance { get; }
        public decimal NewBalance { get; }

        public BalanceChanged(decimal oldBalance, decimal newBalance)
        {
            OldBalance = oldBalance;
            NewBalance = newBalance;
        }
    }

    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message)
        {
        }
    }
}
