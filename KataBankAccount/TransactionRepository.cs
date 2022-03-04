using System;
using System.Collections.Generic;
using System.Text;

namespace KataBankAccount
{
    public interface ITransactionRepository
    {
        void AddDeposit(int amount);
        IReadOnlyList<Transaction> GetAll();
    }

    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public TransactionRepository(IDateTimeProvider dateTimeProvider) => _dateTimeProvider = dateTimeProvider;

        public void AddDeposit(int amount) => _transactions.Add(new Transaction(_dateTimeProvider.FormattedCurrentDate, amount));

        public IReadOnlyList<Transaction> GetAll()
        {
            return _transactions;
        }
    }
}
