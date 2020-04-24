using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizeDLLTests
{
    class UserDataBaseTests
    {
        private static string FilePath = "Test.txt";
        [SetUp]
        public void BeforEach()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
        
        [TearDown]
        public void AfterAll()
        {
            UserDataBase.ClearFile(FilePath);
        }

        [Test]
        public void IsUserRegisteredTestFalse()
        {
            var expected = false;
            var result = UserDataBase.IsUserRegistered(FilePath, "False", "False");

            Assert.AreEqual(expected,result);
        }

        [Test]
        public void IsUserRegisteredTestTrue()
        {
            UserDataBase.RegisterUser(FilePath,"Login","Pa55word","Mikoo","study",5);

            var result = UserDataBase.IsUserRegistered(FilePath, "Login", "Pa55word");
            Assert.IsTrue(result);
            
            UserDataBase.ClearFile("Login"+FilePath);
        }

        [Test]
        public void IsUserRegisteredTestNull()
        {
            var expected = false;
            var result = UserDataBase.IsUserRegistered(FilePath, null, "Pa55word");
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void IsLoginFreePositiveWithEmptyDatabaseTest()
        {
            var result = UserDataBase.IsLoginFree(FilePath, "login");
            Assert.IsTrue(result);
        }
        
        [Test]
        public void IsLoginFreePositiveWithNotEmptyDataBaseTest()
        {
            UserDataBase.RegisterUser(FilePath,"anotherLogin","Pas55ord","name","study",2);
            var result = UserDataBase.IsLoginFree(FilePath, "login");
            Assert.IsTrue(result);
            
            UserDataBase.ClearFile("anotherLogin"+FilePath);
        }
        
        [Test]
        public void IsLoginFreeNegativeTest()
        {
            UserDataBase.RegisterUser(FilePath,"login","Pas55ord","name","study",2);
            var result = UserDataBase.IsLoginFree(FilePath, "login");
            Assert.IsFalse(result);
            
            UserDataBase.ClearFile("login"+FilePath);
        }
    }
}
