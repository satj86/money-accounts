using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Money.Accounts.Parsing.Statements;

namespace Money.Accounts.Parsing.Tests
{
    [TestClass]
    public class MetroBusinessStatementParserTests
    {
        [TestMethod]
        public void Metro_Business_parser_parses_statement_entries()
        {
            //Arrange
            const string statemtentPath = "C:\\SampleStatements\\Transaction_24.06.2018.csv";
            IStatementParser parser = new MetroBusinessStatementParser();

            //Act
            var statementEntries = parser.ReadStatement(statemtentPath);

            //Assert
            DateTime date;
            DateTime.TryParse("22/06/2018", out date);

            Assert.AreEqual(date, statementEntries.First().Date);
            Assert.AreEqual("GRAVITAS RECRUITME", statementEntries.First().Description);
            Assert.AreEqual(3103.44m, statementEntries.First().Amount);
        }
    }
}
