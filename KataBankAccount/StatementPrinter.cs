using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataBankAccount
{
    public interface IStatementPrinter
    {
        void PrintResult(IReadOnlyList<Transaction> transactions);
    }

    public class StatementPrinter : IStatementPrinter
    {
        private readonly IConsole _console;

        private const string StatementHeader = "DATE | MONTANT | BALANCE";
        private const string NumberFormat = "0.00";

        public StatementPrinter(IConsole console) => _console = console;

        public void PrintResult(IReadOnlyList<Transaction> transactions)
        {
            _console.WriteLine(StatementHeader);

            int balance = 0;
            transactions.AsEnumerable()
                .Select(t =>
                {
                    balance += t.Amount;
                    return $"{t.Date} | {t.Amount.ToString(NumberFormat)} | {balance.ToString(NumberFormat)}";
                })
                .Reverse()
                .ToList()
                .ForEach(_console.WriteLine);
        }
    }
}
