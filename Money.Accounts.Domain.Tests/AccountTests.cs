using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Money.Accounts.Parsing.Model;

namespace Money.Accounts.Domain.Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void Account_is_initialised_with_opening_balance()
        {
            //Arrange
            var openingBalance = 23343.76m;
            var account = new Account();

            //Act
            account.SetOpeningBalance(openingBalance);

            //Assert
            Assert.AreEqual(openingBalance, account.Balance);
            Assert.AreEqual("Opening balance", account.Transactions.First().Description);
        }

        [TestMethod]
        public void Adding_statement_entry_updates_balance()
        {
            //Arrange
            var openingBalance = 23343.76m;
            var account = new Account();
            account.SetOpeningBalance(openingBalance);

            var statementEntry = new StatementEntry { Amount = 50m };
            //Act
            account.AddTransaction(statementEntry);

            //Assert
            Assert.AreEqual(23393.76m, account.Balance);
        }
    }
}
