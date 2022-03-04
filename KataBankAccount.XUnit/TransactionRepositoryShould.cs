using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KataBankAccount.XUnit
{
    public class TransactionRepositoryShould
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

        public TransactionRepositoryShould()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _transactionRepository = new TransactionRepository(_dateTimeProviderMock.Object);
        }

        [Fact]
        public void AddDeposit_CreatesANewDeposit()
        {
            _dateTimeProviderMock.SetupGet(d => d.FormattedCurrentDate).Returns("20/01/2022");
            _transactionRepository.AddDeposit(150);

            var transactions = _transactionRepository.GetAll();

            Assert.Single(transactions);
            Assert.StrictEqual(150, transactions[0].Amount);
            Assert.Equal("20/01/2022", transactions[0].Date);
        }

        [Fact]
        public void AddWithdrawal_CreatesANewWithdrawal()
        {
            _dateTimeProviderMock.SetupGet(d => d.FormattedCurrentDate).Returns("20/01/2022");
            _transactionRepository.AddWithdrawal(120);

            var transactions = _transactionRepository.GetAll();

            Assert.NotNull(transactions);
            Assert.StrictEqual(-120, transactions[0].Amount);
            Assert.Equal("20/01/2022", transactions[0].Date);
        }

        [Fact]
        public void GetAll_WithEmptyRepository_ReturnsEmptyList()
        {
            Assert.Empty(_transactionRepository.GetAll());
        }
    }
}
