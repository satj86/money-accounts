using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Money.Accounts.Parsing.Tests
{
    [TestClass]
    public class HsbcCreditCardStatementParserTests
    {
        [TestMethod]
        public void Hsbc_Credit_Card_parser_parses_statement_entries()
        {
            //Arrange
            const string statemtentPath = "C:\\SampleStatements\\TransHist - cc.csv";
            IStatementParser parser = new HsbcAdvanceStatementParser();

            //Act
            var statementEntries = parser.ReadStatement(statemtentPath);

            //Assert
            DateTime date;
            DateTime.TryParse("23/06/2018", out date);

            Assert.AreEqual(date, statementEntries.First().Date);
            Assert.AreEqual("CONNIBURROW            MILTON KEYNES GB", statementEntries.First().Description);
            Assert.AreEqual(-64.00m, statementEntries.First().Amount);
        }
    }
}
