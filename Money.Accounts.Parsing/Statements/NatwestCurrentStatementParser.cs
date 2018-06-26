using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing.Statements
{
    public class NatwestCurrentStatementParser : IStatementParser
    {
        public IEnumerable<StatementEntry> ReadStatement(string path)
        {
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Equals(string.Empty) || line.StartsWith("Date", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                    string desc = "";
                    
                    string pattern = @"\""\'[A-Za-z0-9 \/\-\,]*\""";

                    Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                    MatchCollection matches = rgx.Matches(line);

                    int i = -1;
                    var lineC = line;
                    var list = new List<string>();
                    foreach(Match m in matches)
                    {
                        lineC = lineC.Replace(m.Value, (++i).ToString());
                        list.Add(m.Value);
                    }

                    var values = lineC.Split(',');

                    DateTime date;
                    DateTime.TryParse(values[0], out date);

                    var amountString = values[3].Replace("\"", "");
                    yield return new StatementEntry {
                        Date = date,
                        Description = list[Convert.ToInt32(values[2])].Replace("\"","").Replace("'",""),
                        Amount = Convert.ToDecimal(amountString)
                    };
                }
            }
        }
    }

}
