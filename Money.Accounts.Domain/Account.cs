using System;
using System.Collections.Generic;
using System.Linq;

namespace Money.Accounts.Domain
{
    public class Account
    {
        public List<Transaction> Transactions { get; }

        public Account()
        {
            Transactions = new List<Transaction>();
        }

        public decimal Balance
        {
            get
            {
                return Transactions.Single(x => x.SequenceNumber == Transactions.Max(y => y.SequenceNumber)).Balance;
            }
        }

        public void SetOpeningBalance(decimal openingBalance)
        {
            Transactions.Add(new Transaction
            {
                Description = "Opening balance",
                Balance = openingBalance,
                SequenceNumber = 0
            });
        }

        public void AddTransaction(DateTime date, string description, decimal amount)
        {
            var newBalance = Balance + amount;
            var newSequenceNumber = Transactions.Max(x => x.SequenceNumber) + 1;

            var transaction = new Transaction
            {
                Date = date,
                Description = description,
                Amount = amount,
                Balance = newBalance,
                SequenceNumber = newSequenceNumber
            };

            Transactions.Add(transaction);
        }
    }
}