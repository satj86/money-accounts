using System;
using System.Collections.Generic;
using System.IO;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing
{
    public class HsbcAdvanceStatementParser : IStatementParser
    {
        public IEnumerable<StatementEntry> ReadStatement(string path)
        {
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    
                    DateTime date;
                    DateTime.TryParse(values[0], out date);

                    var amountString = values[2].Replace("\"", "");

                    yield return new StatementEntry {
                        Date = date,
                        Description = values[1],
                        Amount = Convert.ToDecimal(amountString)
                    };
                }
            }
        }
    }

}
