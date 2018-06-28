using System;
using System.Collections.Generic;
using System.Linq;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Domain.Tests
{
    internal class Account
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
                return Transactions.Last().Balance;
            }
        }

        public void SetOpeningBalance(decimal openingBalance)
        {
            Transactions.Add(new Transaction
            {
                Description = "Opening balance",
                Balance = openingBalance
            });
        }

        internal void AddTransaction(StatementEntry statementEntry)
        {
            var newBalance = Balance + statementEntry.Amount;
            var transaction = new Transaction { Amount = statementEntry.Amount, Balance = newBalance};

            Transactions.Add(transaction);
        }
    }
}