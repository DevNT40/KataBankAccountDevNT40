using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KataBankAccount.XUnit
{
    public class AccountServiceShould
    {
        private readonly AccountService _accountService;
        private readonly Mock<ITransactionRepository> _transactionRepository;
        private readonly Mock<IStatementPrinter> _statementPrinter;

        public AccountServiceShould()
        {
            _transactionRepository = new Mock<ITransactionRepository>();
            _statementPrinter = new Mock<IStatementPrinter>();
            _accountService = new AccountService(_transactionRepository.Object, _statementPrinter.Object);
        }

        [Fact]
        public void Deposit_StoresADepositTransaction()
        {
            _accountService.Deposit(1000);
            _transactionRepository.Verify(r => r.AddDeposit(1000), Times.Once);
        }

        [Fact]
        public void Withdraw_StoresAWithdrawalTransaction()
        {
            _accountService.Withdraw(150);
            _transactionRepository.Verify(r => r.AddWithdrawal(150), Times.AtMostOnce);
        }

        [Fact]
        public void PrintStatement_PrintsTransactions()
        {
            var transactions = new List<Transaction>()
            {
                Deposit("22/01/2022", 1200),
                Withdrawal("12/02/2022", 300),
                Deposit("13/01/2022", 500)
            };
            _transactionRepository.Setup(r => r.GetAll()).Returns(transactions);

            _accountService.PrintStatement();

            _statementPrinter.Verify(p => p.PrintResult(transactions));
        }

        private Transaction Deposit(string date, int amount)
        {
            return new Transaction(date, amount);
        }

        private Transaction Withdrawal(string date, int amount)
        {
            return new Transaction(date, -amount);
        }
    }
}
