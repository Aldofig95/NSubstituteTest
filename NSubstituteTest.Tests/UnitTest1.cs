using System;
using NSubstitute;
using Xunit;

namespace NSubstituteTest.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void Deposit_IncreasesBalance()
        {
            var bankAccount = Substitute.For<IBank>();
            bankAccount.Balance.Returns(100m);

            bankAccount.Deposit(50m);
            bankAccount.Balance = 150m;

            bankAccount.Received().Deposit(50m);
            Assert.Equal(150m, bankAccount.Balance);
        }

        [Fact]
        public void Withdraw_DecreasesBalance()
        {
            var bankAccount = Substitute.For<IBank>();
            bankAccount.Balance.Returns(200m);

            bankAccount.Withdraw(75m);
            bankAccount.Balance = 125m;

            bankAccount.Received().Withdraw(75m);
            Assert.Equal(125m, bankAccount.Balance);
        }

        [Fact]
        public void Withdraw_Exception_IfBalanceIsLow()
        {
            var bankAccount = Substitute.For<IBank>();
            bankAccount.When(x => x.Withdraw(Arg.Any<decimal>())).Do(x =>
            {
                if (bankAccount.Balance < (decimal)x[0])
                    throw new InsufficientFundsException("Insufficient funds");
            });
            bankAccount.Balance.Returns(50m);

            Assert.Throws<InsufficientFundsException>(() => bankAccount.Withdraw(75m));
        }

        [Fact]
        public void BalanceChangedEvent_IsRaisedOnDeposit()
        {
            var bankAccount = Substitute.For<IBank>();
            var oldBalance = 100m;
            var newBalance = 150m;

            bankAccount.When(x => x.Deposit(50m)).Do(_ =>
            {
                bankAccount.BalanceChanged += Raise.EventWith(bankAccount, new BalanceChanged(oldBalance, newBalance));
            });

            EventHandler<BalanceChanged> handler = Substitute.For<EventHandler<BalanceChanged>>();
            bankAccount.BalanceChanged += handler;
            bankAccount.Deposit(50m);

            handler.Received().Invoke(bankAccount, Arg.Is<BalanceChanged>(args => args.OldBalance == oldBalance && args.NewBalance == newBalance));
        }
    }
}
