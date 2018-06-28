namespace Money.Accounts.Domain.Tests
{
    internal class Transaction
    {
        public decimal Balance { get; internal set; }
        public string Description { get; internal set; }
        public decimal Amount { get; internal set; }
    }
}