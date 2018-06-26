using System.Collections.Generic;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing
{
    public interface IBusinessExpenseParser
    {
        IEnumerable<BusinessExpense> ReadExpenses(string path);
    }
}
