using System.Security.Cryptography;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizeDLLTests
{
    [TestFixture]
    public class UpdateUserTest
    {
        private ConnectionToDb dbConnection;
        private User _user;
        private UpdatesTransmitter _transmitter;

        [SetUp]
        public void SetUp()
        {
            dbConnection = new ConnectionToDb();
            dbConnection.InsertUser("TestLogin", "Pa55word", "TestName", 2, "TestStudy");
            _user = dbConnection.GetUser("TestLogin", "Pa55word");
            _transmitter = new UpdatesTransmitter(_user, dbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            dbConnection.DeleteUser(_user._userId);
        }

        [Test]
        public void UpdateLoginWithNullTest()
        {
            _transmitter.UpdateLogin(null);
            string oldLogin = _user.Login;
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newLogin = userAfterUpdates.Login;
            Assert.That(oldLogin, Is.EqualTo(newLogin));
        }
        
        [Test]
        public void UpdateLoginWithEmptyStringTest()
        {
            _transmitter.UpdateLogin("");   
            string oldLogin = _user.Login;
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newLogin = userAfterUpdates.Login;
            Assert.That(oldLogin, Is.EqualTo(newLogin));
        }
        
        [Test]
        public void UpdateLoginWithValidLoginTest()
        {
            _transmitter.UpdateLogin("ValidLogin");       
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string expected = "ValidLogin";
            string newLogin = userAfterUpdates.Login;
            Assert.That(expected, Is.EqualTo(newLogin));
        }
        
        [Test]
        public void UpdatePasswordWithNullTest()
        {
            Assert.Throws<InvalidInputException>(() => _transmitter.UpdatePassword(null));
            string oldPassword = _user.Password;
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newPassword = userAfterUpdates.Password;
            Assert.That(oldPassword, Is.EqualTo(newPassword));
        }
        
        [Test]
        public void UpdatePasswordWithEmptyPasswordTest()
        {
            Assert.Throws<InvalidInputException>(() => _transmitter.UpdatePassword(""));
            string oldPassword = _user.Password;
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newPassword = userAfterUpdates.Password;
            Assert.That(oldPassword, Is.EqualTo(newPassword));
        }
            
        [Test]
        public void UpdatePassWithInvalidPasswordTest()
        {
            Assert.Throws<InvalidInputException>(() => _transmitter.UpdatePassword("invalidPassword"));
            string oldPassword = _user.Password;
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newPassword = userAfterUpdates.Password;
            Assert.That(oldPassword, Is.EqualTo(newPassword));
        }
        
        [Test]
        public void UpdatePasswordWithValidPasswordTest()
        {
            _transmitter.UpdatePassword("ValidPa55word");       
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            using (MD5 hasher = MD5.Create())
            {
                string expected = "ValidPa55word";
                string newLogin = userAfterUpdates.Password;
                Assert.That(expected, Is.EqualTo(newLogin));
            }
        }
        
        [Test]
        public void UpdateNameWithNullTest()
        {
            _transmitter.UpdateName(null);
            string oldName = _user.Name;
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newName = userAfterUpdates.Name;
            Assert.That(oldName, Is.EqualTo(newName));
        }
        
        [Test]
        public void UpdateNameWithEmptyStringTest()
        {
            _transmitter.UpdateName("");   
            string oldName = _user.Name;
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newName = userAfterUpdates.Name;
            Assert.That(oldName, Is.EqualTo(newName));
        }
        
        [Test]
        public void UpdateNameWithValidNameTest()
        {
            _transmitter.UpdateName("ValidName");       
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string expected = "ValidName";
            string newName = userAfterUpdates.Name;
            Assert.That(expected, Is.EqualTo(newName));
        }
        
        [Test]
        public void UpdateSemesterWithValidIntTest()
        {
            _transmitter.UpdateSemester(5);
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            int newSemester = userAfterUpdates.Semester;
            Assert.That(5, Is.EqualTo(newSemester));
        }
        
        [Test]
        public void UpdateNameWithZeroTest()
        {
            _transmitter.UpdateSemester(0);
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            int newSemester = userAfterUpdates.Semester;
            Assert.That(2, Is.EqualTo(newSemester));
        }
        
        [Test]
        public void UpdateNameWithValueLesserThanZeroTest()
        {
            _transmitter.UpdateSemester(-1);
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            int newSemester = userAfterUpdates.Semester;
            Assert.That(2, Is.EqualTo(newSemester));
        }
        
        [Test]
        public void UpdateStudyWithNullTest()
        {
            string oldStudy = _user.Study;
            _transmitter.UpdateStudy(null);
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newStudy = userAfterUpdates.Study;
            Assert.That(oldStudy, Is.EqualTo(newStudy));
        }
        
        [Test]
        public void UpdateStudyWithEmptyStringTest()
        {
            string oldStudy = _user.Study;
            _transmitter.UpdateStudy("");
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newStudy = userAfterUpdates.Study;
            Assert.That(oldStudy, Is.EqualTo(newStudy));
        }
        
        [Test]
        public void UpdateStudyWithValidStringTest()
        {
            _transmitter.UpdateStudy("ValidStudy");
            var userAfterUpdates = dbConnection.GetUser(_user.Login, _user.Password);
            string newStudy = userAfterUpdates.Study;
            Assert.That("ValidStudy", Is.EqualTo(newStudy));
        }
    }
}