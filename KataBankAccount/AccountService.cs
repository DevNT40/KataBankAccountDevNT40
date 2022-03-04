using System;
using System.Collections.Generic;
using System.Text;

namespace KataBankAccount
{
    public class AccountService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IStatementPrinter _statementPrinter;

        public AccountService(ITransactionRepository transactionRepository, IStatementPrinter statementPrinter)
        {
            _transactionRepository = transactionRepository;
            _statementPrinter = statementPrinter;
        }

        public void Deposit(int amount)
        {
            if (amount > 0)
                _transactionRepository.AddDeposit(amount);
        }

        public void Withdraw(int amount)
        {
            if (_transactionRepository.GetBallance() > amount)
                _transactionRepository.AddWithdrawal(amount);
        }
        public void PrintStatement() => _statementPrinter.PrintResult(_transactionRepository.GetAll());
    }
}
