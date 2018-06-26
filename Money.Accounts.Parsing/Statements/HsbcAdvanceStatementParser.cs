using System;
using System.Collections.Generic;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing.Statements
{
    public class HsbcAdvanceStatementParser : IStatementParser
    {
        private readonly ICsvFileReader _csvFileReader;

        public HsbcAdvanceStatementParser(ICsvFileReader csvFileReader)
        {
            _csvFileReader = csvFileReader;
        }

        public IEnumerable<StatementEntry> ReadStatement(string path)
        {
            var statementEntries = _csvFileReader.MapRows<StatementEntry>(path, (statementEntry, rowValues) => {
                DateTime date;
                DateTime.TryParse(rowValues[0], out date);

                statementEntry.Date = date;
                statementEntry.Description = rowValues[1];

                var amountString = rowValues[2].Replace("\"", "");
                statementEntry.Amount = Convert.ToDecimal(amountString);
            });

            return statementEntries;
        }
    }

}
