using System;
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
        public void Adding_transaction_calculates_balance_correctly()
        {
            //Arrange
            var openingBalance = 23343.76m;
            var account = new Account();
            account.SetOpeningBalance(openingBalance);

            var statementEntry = new StatementEntry { Amount = 50m };
            //Act
            account.AddTransaction(statementEntry.Date, statementEntry.Description, statementEntry.Amount);

            //Assert
            Assert.AreEqual(23393.76m, account.Transactions[1].Balance);
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
            account.AddTransaction(statementEntry.Date, statementEntry.Description, statementEntry.Amount);

            //Assert
            Assert.AreEqual(23393.76m, account.Balance);
        }

        [TestMethod]
        public void Order_of_adding_new_transactions_on_same_date_is_maintainted()
        {
            //Arrange
            var openingBalance = 23343.76m;
            var account = new Account();
            account.SetOpeningBalance(openingBalance);

            var statementEntryA = new StatementEntry { Date = DateTime.Today, Amount = 50m };
            var statementEntryB = new StatementEntry { Date = DateTime.Today, Amount = 100m };
            var statementEntryC = new StatementEntry { Date = DateTime.Today, Amount = 5m };

            //Act
            account.AddTransaction(statementEntryA.Date, statementEntryA.Description, statementEntryA.Amount);
            account.AddTransaction(statementEntryB.Date, statementEntryB.Description, statementEntryB.Amount);
            account.AddTransaction(statementEntryC.Date, statementEntryC.Description, statementEntryC.Amount);

            //Assert
            Assert.AreEqual(1, account.Transactions[1].SequenceNumber);
            Assert.AreEqual(statementEntryA.Amount, account.Transactions[1].Amount);
            Assert.AreEqual(statementEntryA.Date, account.Transactions[1].Date);

            Assert.AreEqual(2, account.Transactions[2].SequenceNumber);
            Assert.AreEqual(statementEntryB.Amount, account.Transactions[2].Amount);
            Assert.AreEqual(statementEntryB.Date, account.Transactions[2].Date);

            Assert.AreEqual(3, account.Transactions[3].SequenceNumber);
            Assert.AreEqual(statementEntryC.Amount, account.Transactions[3].Amount);
            Assert.AreEqual(statementEntryC.Date, account.Transactions[3].Date);
        }

        [TestMethod]
        public void Transaction_details_are_added_from_statement_entry()
        {
            //Arrange
            var openingBalance = 23343.76m;
            var account = new Account();
            account.SetOpeningBalance(openingBalance);

            var statementEntryA = new StatementEntry
            {
                Date = DateTime.Today,
                Amount = 50m,
                Description = "Transfer from X"
            };

            //Act
            account.AddTransaction(statementEntryA.Date, statementEntryA.Description, statementEntryA.Amount);

            //Assert
            Assert.AreEqual(statementEntryA.Amount, account.Transactions[1].Amount);
            Assert.AreEqual(statementEntryA.Date, account.Transactions[1].Date);
            Assert.AreEqual(statementEntryA.Description, account.Transactions[1].Description);
        }
    }
}
