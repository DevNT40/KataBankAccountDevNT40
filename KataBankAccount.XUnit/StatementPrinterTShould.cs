using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace KataBankAccount.XUnit
{
    public class StatementPrinterTShould
    {
        private readonly Mock<IConsole> _console;
        private readonly StatementPrinter _statementPrinter;

        private readonly IReadOnlyList<Transaction> _noTransactions = new List<Transaction>();

        public StatementPrinterTShould()
        {
            _console = new Mock<IConsole>();
            _statementPrinter = new StatementPrinter(_console.Object);
        }

        [Fact]
        public void ShouldAlwaysPrintsHeader()
        {
            _statementPrinter.PrintResult(_noTransactions);
            _console.Verify(c => c.WriteLine("DATE | MONTANT | BALANCE"));
            _console.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldPrintsTransactionsInReverseDateOrder()
        {
            var transactions = new List<Transaction>()
            {
                Deposit("12/01/2022", 1700),
                Withdrawal("15/02/2022", 450),
                Deposit("04/03/2022", 1575)
            };

            List<string> lines = new List<string>();
            _console.Setup(c => c.WriteLine(It.IsAny<string>())).Callback<string>(line => lines.Add(line));
            _statementPrinter.PrintResult(transactions);

            Assert.Equal(4, lines.Count);
            Assert.Equal("DATE | MONTANT | BALANCE", lines[0]);
            Assert.Equal("04/03/2022 | 1575,00 | 2825,00", lines[1]);
            Assert.Equal("15/02/2022 | -450,00 | 1250,00", lines[2]);
            Assert.Equal("12/01/2022 | 1700,00 | 1700,00", lines[3]);
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
