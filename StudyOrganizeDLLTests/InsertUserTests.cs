using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;

namespace StudyOrganizeDLLTests
{
    [TestFixture]
    public class InsertUserTests
    {
        private ConnectionToDb dbConnection;

        [SetUp]
        public void SetUp()
        {
            dbConnection = new ConnectionToDb();
        }

        [Test]
        public void InsertWithValidData()
        {
            dbConnection.InsertUser("TestLogin", "Pa55word", "TestName", 2, "TestStudy");
            var user = dbConnection.GetUser("TestLogin", "Pa55word");
            Assert.NotNull(user);
            Assert.That(user.Name, Is.EqualTo("TestName"));
            dbConnection.DeleteUser(user._userId);
            Assert.Throws<UserNotInDatabaseException>(() => dbConnection.GetUser("TestLogin", "Pa55word"));
        }
        
        [Test]
        public void InsertWithEmptyLogin()
        {
            Assert.Throws<InvalidInputException>(() => dbConnection.InsertUser("", "Pa55word", "TestName", 2, "TestStudy"));
        }
        
        [Test]
        public void InsertWithNullLogin()
        {
            Assert.Throws<InvalidInputException>(() => dbConnection.InsertUser(null, "Pa55word", "TestName", 2, "TestStudy"));
        }
        
        [Test]
        public void InsertWithEmptyPassword()
        {
            Assert.Throws<InvalidInputException>(() => dbConnection.InsertUser("TestLogin", "", "TestName", 2, "TestStudy"));
        }
        
        [Test]
        public void InsertWithNullPassword()
        {
            Assert.Throws<InvalidInputException>(() => dbConnection.InsertUser("TestLogin", null, "TestName", 2, "TestStudy"));
        }
        
        [Test]
        public void InsertWithEmptyName()
        {
            Assert.Throws<InvalidInputException>(() => dbConnection.InsertUser("TestLogin", "Pa55word", "", 2, "TestStudy"));
        }
        
        [Test]
        public void InsertWithNullName()
        {
            Assert.Throws<InvalidInputException>(() => dbConnection.InsertUser("TestLogin", "Pa55word", null, 2, "TestStudy"));
        }
    }
}