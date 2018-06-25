using System.Collections.Generic;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing
{
    public interface IStatementParser
    {
        IEnumerable<StatementEntry> ReadStatement(string path);
    }
}
