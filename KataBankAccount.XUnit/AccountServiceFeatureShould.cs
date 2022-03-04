using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KataBankAccount.XUnit
{
    public class AccountServiceFeatureShould
    {
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
        private readonly Mock<IConsole> _consoleMock;
        private readonly AccountService _accountService;

        public AccountServiceFeatureShould()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _consoleMock = new Mock<IConsole>();
            _accountService = new AccountService(new TransactionRepository(_dateTimeProviderMock.Object), new StatementPrinter(_consoleMock.Object));
        }

        [Fact]
        public void PrintStatement_ContainsAllTransactions()
        {
            _dateTimeProviderMock.SetupSequence(d => d.FormattedCurrentDate)
                .Returns("25/01/2022")
                .Returns("12/02/2022")
                .Returns("27/02/2022");

            var lines = new List<string>();
            _consoleMock.Setup(c => c.WriteLine(It.IsAny<string>()))
                .Callback<string>(line => lines.Add(line));

            _accountService.Deposit(1400);
            _accountService.Withdraw(300);
            _accountService.Deposit(500);
            _accountService.PrintStatement();

            Assert.Equal(4, lines.Count);
            Assert.Equal("DATE | MONTANT | BALANCE", lines[0]);
            Assert.Equal("27/02/2022 | 500,00 | 1600,00", lines[1]);
            Assert.Equal("12/02/2022 | -300,00 | 1100,00", lines[2]);
            Assert.Equal("25/01/2022 | 1400,00 | 1400,00", lines[3]);
        }
    }
}
