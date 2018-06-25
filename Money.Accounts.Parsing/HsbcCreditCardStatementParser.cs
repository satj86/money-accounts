using System;
using System.Collections.Generic;
using System.Text;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing
{
    class HsbcCreditCardStatementParser : IStatementParser
    {
        public IEnumerable<StatementEntry> ReadStatement(string path)
        {
            throw new NotImplementedException();
        }
    }
}
