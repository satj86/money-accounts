using System;
using System.Collections.Generic;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing.Statements
{
    public class MetroBusinessStatementParser : IStatementParser
    {
        private readonly ICsvFileReader _csvFileReader;

        public MetroBusinessStatementParser(ICsvFileReader csvFileReader)
        {
            _csvFileReader = csvFileReader;
        }

        public IEnumerable<StatementEntry> ReadStatement(string path)
        {
            const string headerRow = "Date,Reference,Transaction Type,Money In, Money Out,Balance";

            var statementEntries = _csvFileReader.MapRows<StatementEntry>(path, headerRow, (statementEntry, rowValues) => {
                DateTime date;
                DateTime.TryParse(rowValues[0], out date);

                var moneyIn = Convert.ToDecimal(rowValues[3]);
                var moneyOut = Convert.ToDecimal(rowValues[4]);

                statementEntry.Date = date;
                statementEntry.Description = rowValues[1];
                statementEntry.Amount = moneyIn > 0 ? moneyIn : -moneyOut;
            });

            return statementEntries;      
        }
    }
}
