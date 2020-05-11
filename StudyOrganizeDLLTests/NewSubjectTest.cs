using System;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizeDLLTests
{
    [TestFixture]
    public class NewSubjectTest
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
            dbConnection.InsertSchoolTask(0, "TestTittle", "TestDescription", 0, false, DateTime.Now);
            var loadedFromDb = dbConnection.GetSchoolTask("TestTittle");
            Assert.AreEqual("TestDescription", loadedFromDb.Description);
            Assert.AreEqual(TaskGroup.Planned, loadedFromDb.TaskGroup);
            dbConnection.DeleteSchoolTask(loadedFromDb.Id);
        }
        [Test]
        public void AddSubjectWithTheSameNameTwiceShouldGetException()    
        {
            dbConnection.InsertSchoolTask(0, "TestTittle", "TestDescription", 0, false, DateTime.Now);
            Assert.Throws<MySqlException>(() => 
                dbConnection.InsertSchoolTask(0, "TestTittle", "TestDescription", 0, false, DateTime.Now));
            var loadedFromDb = dbConnection.GetSchoolTask("TestTittle");
            Assert.AreEqual("TestDescription", loadedFromDb.Description);
            Assert.AreEqual(TaskGroup.Planned, loadedFromDb.TaskGroup);
            dbConnection.DeleteSchoolTask(loadedFromDb.Id);
        }
    }
}