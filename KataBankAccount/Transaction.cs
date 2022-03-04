using System;
using System.Collections.Generic;
using System.Text;

namespace KataBankAccount
{
    public class Transaction
    {
        public int Amount { get; }
        public string Date { get; }

        public Transaction(string date, int amount)
        {
            Date = date;
            Amount = amount;
        }
    }
}
