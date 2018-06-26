using System;
using System.Collections.Generic;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Parsing.BusinessExpenses
{
    public class CrunchBusinessExpenseParser : IBusinessExpenseParser
    {
        private readonly ICsvFileReader _csvFileReader;

        public CrunchBusinessExpenseParser(ICsvFileReader csvFileReader)
        {
            _csvFileReader = csvFileReader;
        }

        public IEnumerable<BusinessExpense> ReadExpenses(string path)
        {
            const string headerRow = "Date,Supplier - Ref,Recharge to,Net amount,VAT amount,Gross amount,Payment(s) amount,Combined,Payment method(s),Credit note(s) amount,Payment status,Attachments,Line item(s) description";
            var expenses = _csvFileReader.MapRows<BusinessExpense>(path, headerRow, (businessExpense, rowValues) => {
                DateTime date;
                DateTime.TryParse(rowValues[0], out date);
                
                businessExpense.Date = date;
                businessExpense.Description = rowValues[12];
                businessExpense.NetAmount = Convert.ToDecimal(rowValues[3]);
                businessExpense.VatAmount = Convert.ToDecimal(rowValues[4]);
                businessExpense.GrossAmount = Convert.ToDecimal(rowValues[5]);
                businessExpense.Supplier = rowValues[1];                
            });

            return expenses;
        }
    }
}
