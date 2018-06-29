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

        internal void AddTransaction(StatementEntry statementEntry)
        {
            var newBalance = Balance + statementEntry.Amount;
            var newSequenceNumber = Transactions.Max(x => x.SequenceNumber) + 1;

            var transaction = new Transaction
            {
                Date = statementEntry.Date,
                Description = statementEntry.Description,
                Amount = statementEntry.Amount,
                Balance = newBalance,
                SequenceNumber = newSequenceNumber
            };

            Transactions.Add(transaction);
        }
    }
}