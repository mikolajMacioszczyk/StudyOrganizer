using System;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;

namespace StudyOrganizeDLLTests
{
    [TestFixture]
    public class NewSchoolTaskTest
    {
        private ConnectionToDb dbConnection;
        [SetUp]
        public void SetUp()
        {    
            dbConnection = new ConnectionToDb();
        }

        [Test]
        public void AddNullSubjectNotingShouldBeAdded()
        {
            Assert.Throws<InvalidInputException>(() =>
                dbConnection.InsertSchoolTask(0, null, null, 0, false, DateTime.Now));
        }
        [Test]
        public void AddSubjectWithEmptyStringsNotingShouldBeAdded()
        {
            Assert.Throws<InvalidInputException>(() => 
                dbConnection.InsertSchoolTask(0, "", "", 0, false, DateTime.Now));
        }
        [Test]
        public void AddValidSubject()
        {
            dbConnection.InsertSubject(0, "TestSubject", 0, "Monday", 5);
            var loadedFromDb = dbConnection.GetSubject("TestSubject");
            Assert.AreEqual("Monday", loadedFromDb.Day.ToString());
            Assert.AreEqual(5, loadedFromDb.Hour);
            dbConnection.DeleteSubject(loadedFromDb.Id);
        }
        [Test]
        public void AddSubjectWithTheSameNameTwiceShouldGetException()    
        {
            dbConnection.InsertSubject(0, "TestSubject", 0, "Monday", 5);
            Assert.Throws<InvalidOperationException>(() => 
            dbConnection.InsertSubject(0, "TestSubject", 0, "Monday", 5));
            var loadedFromDb = dbConnection.GetSubject("TestSubject");
            Assert.AreEqual("Monday", loadedFromDb.Day.ToString());
            Assert.AreEqual(5, loadedFromDb.Hour);
            dbConnection.DeleteSubject(loadedFromDb.Id);
        }
    }
}