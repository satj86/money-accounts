using System;
using System.Collections.Generic;
using System.Text;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing
{
    public class MetroBusinessStatementParser : IStatementParser
    {
        public IEnumerable<StatementEntry> ReadStatement(string path)
        {
            throw new NotImplementedException();
        }
    }
}
