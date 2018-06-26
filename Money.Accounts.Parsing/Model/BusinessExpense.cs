using System;

namespace Money.Accounts.Parsing.Model
{
    public class BusinessExpense
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public string Supplier { get; set; }
    }
}