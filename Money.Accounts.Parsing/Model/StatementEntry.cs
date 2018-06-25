using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Accounts.Parsing.Model
{
    public class StatementEntry
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
