using System;
using System.Collections.Generic;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing.Statements
{
    public class NatwestCurrentStatementParser : IStatementParser
    {
        private readonly ICsvFileReader _csvFileReader;

        public NatwestCurrentStatementParser(ICsvFileReader csvFileReader)
        {
            _csvFileReader = csvFileReader;
        }

        public IEnumerable<StatementEntry> ReadStatement(string path)
        {
            const string headerRow = "Date, Type, Description, Value, Balance, Account Name, Account Number";
            const string treatAsColumnPattern = @"\""\'[A-Za-z0-9 \/\-\,]*\""";

            var statementEntries = _csvFileReader.MapRows<StatementEntry>(path, headerRow, treatAsColumnPattern, (statementEntry, rowValues) => {
                DateTime date;
                DateTime.TryParse(rowValues[0], out date);

                var amountString = rowValues[3].Replace("\"", "");

                statementEntry.Date = date;
                statementEntry.Description =  rowValues[2].Replace("\"", "").Replace("'", "");
                statementEntry.Amount = Convert.ToDecimal(amountString);               
            });

            return statementEntries;
        }
    }

}
