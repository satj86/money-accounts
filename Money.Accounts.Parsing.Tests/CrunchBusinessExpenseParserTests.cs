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
            IBusinessExpenseParser parser = new CrunchBusinessExpenseParser(new CsvFileReader());

            //Act
            var expenses = parser.ReadExpenses(statementPath);

            //Assert
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
