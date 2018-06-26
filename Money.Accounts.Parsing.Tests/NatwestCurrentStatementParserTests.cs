using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Money.Accounts.Parsing.Tests
{
    [TestClass]
    public class NatwestCurrentStatementParserTests
    {
        [TestMethod]
        public void NatWest_Current_parser_parses_statement_entries()
        {
            //Arrange
            const string statemtentPath = "C:\\SampleStatements\\JOOTLESSSTU0934526706-20180624.csv";
            IStatementParser parser = new NatwestCurrentStatementParser();

            //Act
            var statementEntries = parser.ReadStatement(statemtentPath);

            //Assert
            DateTime date;
            DateTime.TryParse("18/06/2018", out date);

            Assert.AreEqual(date, statementEntries.First().Date);
            Assert.AreEqual("NAIK A , FOOS RETURN , VIA MOBILE - PYC", statementEntries.First().Description);
            Assert.AreEqual(80.00m, statementEntries.First().Amount);
        }
    }
}
