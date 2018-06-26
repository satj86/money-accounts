using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Money.Accounts.Parsing.BusinessExpenses;

namespace Money.Accounts.Parsing.Tests
{
    [TestClass]
    public class CrunchBusinessExpenseParserTests
    {
        [TestMethod]
        public void Crunch_expenses_parser_parses_statement_entries()
        {
            //Arrange
            const string statementPath = "C:\\SampleStatements\\Expenses.csv";
            IBusinessExpenseParser parser = new CrunchBusinessExpenseParser();

            //Act
            var expenses = parser.ReadExpenses(statementPath);

            //Assert
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
            //21/06/2018,Food - Breakfast,N/A,2.37,0.48,2.85,2.85,false,Paid by director personally,0.00,Paid,1,Breakfast
            DateTime date;
            DateTime.TryParse("21/06/2018", out date);

            Assert.AreEqual(date, expenses.First().Date);
            Assert.AreEqual("Breakfast", expenses.First().Description);
            Assert.AreEqual(2.37m, expenses.First().NetAmount);
            Assert.AreEqual(0.48m, expenses.First().VatAmount);
            Assert.AreEqual(2.85m, expenses.First().GrossAmount);
            Assert.AreEqual("Food - Breakfast", expenses.First().Supplier);
        }
    }
}
