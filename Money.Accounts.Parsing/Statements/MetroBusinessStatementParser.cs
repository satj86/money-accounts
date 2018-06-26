using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing.Statements
{
    public class MetroBusinessStatementParser : IStatementParser
    {
        public IEnumerable<StatementEntry> ReadStatement(string path)
        {
            //Date,Reference,Transaction Type,Money In, Money Out,Balance

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

                    var moneyIn = Convert.ToDecimal(values[3]);
                    var moneyOut = Convert.ToDecimal(values[4]);

                    yield return new StatementEntry
                    {
                        Date = date,
                        Description = values[1],
                        Amount = moneyIn > 0 ? moneyIn : -moneyOut
                    };
                }
            }
        }
    }
}
