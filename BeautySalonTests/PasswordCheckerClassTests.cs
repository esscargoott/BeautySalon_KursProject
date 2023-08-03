using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeautySalon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Tests
{
    [TestClass()]
    public class PasswordCheckerClassTests
    {
        [TestMethod()]
        public void Correct_ReturnsTrue()
        {
            //Arrange.
             string password = "admin1";
            bool expected = true;
            //Act.
            bool actual = PasswordCheckerClass.ValidatePassword(password);
            //Assert.
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Check_10Symbols_ReturnsFalse()
        {
            //Arrange.
            string password = "admin12345";
            bool expected = false;
            //Act.
            bool actual = PasswordCheckerClass.ValidatePassword(password);
            //Assert.
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Check_PasswordWithoutLowerSymbols_ReturnsFalse()
        {
            //Arrange.
            string password = "ADMIN2";
            bool expected = false;
            //Act.
            bool actual = PasswordCheckerClass.ValidatePassword(password);
            //Assert.
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Check_PasswordAllDigits_ReturnsFalse()
        {
            //Arrange.
            string password = "1920394";
            bool expected = false;
            //Act.
            bool actual = PasswordCheckerClass.ValidatePassword(password);
            //Assert.
            Assert.AreEqual(expected, actual);
        }
    }
}