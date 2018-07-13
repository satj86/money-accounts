using System;

namespace Money.Accounts.Domain
{
    public class Transaction
    {
        public decimal Balance { get; internal set; }
        public string Description { get; internal set; }
        public DateTime Date { get; internal set; }
        public int SequenceNumber { get; internal set; }
        public decimal Amount { get; internal set; }
    }
}