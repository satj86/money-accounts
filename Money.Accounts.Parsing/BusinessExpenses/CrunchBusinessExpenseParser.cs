using System;
using System.Collections.Generic;
using System.IO;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing.BusinessExpenses
{
    public class CrunchBusinessExpenseParser : IBusinessExpenseParser
    {
        public IEnumerable<BusinessExpense> ReadExpenses(string path)
        {
            /*
             * 0  Date,
             * 1  Supplier - Ref,
             * 2  Recharge to,
             * 3  Net amount,
             * 4  VAT amount,
             * 5  Gross amount,
             * 6  Payment(s) amount,
             * 7  Combined,
             * 8  Payment method(s),
             * 9  Credit note(s) amount,
             * 10 Payment status,
             * 11 Attachments,
             * 12 Line item(s) description
             */
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Equals(string.Empty) || line.StartsWith("Date", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                    var values = line.Split(',');

                    DateTime date;
                    DateTime.TryParse(values[0], out date);
                    
                    yield return new BusinessExpense
                    {
                        Date = date,
                        Description = values[12],
                        NetAmount = Convert.ToDecimal(values[3]),
                        VatAmount = Convert.ToDecimal(values[4]),
                        GrossAmount = Convert.ToDecimal(values[5]),
                        Supplier = values[1]
                    };
                }
            }
        }
    }
}
