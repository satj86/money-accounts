using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Money.Accounts.Parsing.Statements;

namespace Money.Accounts.Parsing.Tests
{
    [TestClass]
    public class HsbcAdvanceStatementParserTests
    {
        [TestMethod]
        public void Hsbc_Advance_parser_parses_statement_entries()
        {
            //Arrange
            const string statemtentPath = "C:\\SampleStatements\\TransHist regular.csv";
            IStatementParser parser = new HsbcAdvanceStatementParser();

            //Act
            var statementEntries = parser.ReadStatement(statemtentPath);

            //Assert
            DateTime date;
            DateTime.TryParse("19/06/2018", out date);

            Assert.AreEqual(date, statementEntries.First().Date);
            Assert.AreEqual("F MARTINA                Metal Fest                BP", statementEntries.First().Description);
            Assert.AreEqual(-42.00m, statementEntries.First().Amount);
        }
    }
}
