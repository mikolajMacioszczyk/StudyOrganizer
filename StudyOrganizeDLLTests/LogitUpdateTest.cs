using System;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;

namespace StudyOrganizeDLLTests
{
    [TestFixture]
    public class LogitUpdateTest
    {
        private static string FilePath = "TestLiginUpdate.txt";

        [SetUp]
        public void BeforeEach()
        {
            UserDataBase.RegisterUser(FilePath, "login","password","name","Study",5);
        }
        
        [TearDown]
        public void AfterEach()
        {
            UserDataBase.ClearFile(FilePath);
        }
        
        [Test]
        public void UpdateWithTheSameLogin()
        {
            UserDataBase.UpdateLogin(FilePath,"login","login");
            var result = UserDataBase.IsUserRegistered(FilePath, "login", "password");
            Assert.IsTrue(result);
        }
        
        [Test]
        public void UpdateWithNull()
        {
            Assert.Throws<InvalidInputException>(() => { UserDataBase.UpdateLogin(FilePath,null,"login"); });
        }
        
        [Test]
        public void UpdateWithExistingLogin()
        {
            UserDataBase.RegisterUser(FilePath, "login2","password2","name2","Study2",5);

            Assert.Throws<InvalidInputException>(() => { UserDataBase.UpdateLogin(FilePath, "login2", "login"); });
        }
        
        [Test]
        public void UpdateNotExistingLogin()
        {

            Assert.Throws<InvalidInputException>(() => { UserDataBase.UpdateLogin(FilePath, "login2", "login3"); });
        }
        
        [Test]
        public void UpdateLoginPositive()
        {
            UserDataBase.UpdateLogin(FilePath,"newLogin","login");
            var result = UserDataBase.IsUserRegistered(FilePath, "login", "password");
            Assert.IsFalse(result);
            
            var result2 = UserDataBase.IsUserRegistered(FilePath, "newLogin", "password");
            Assert.IsTrue(result2);
        }
    }
}