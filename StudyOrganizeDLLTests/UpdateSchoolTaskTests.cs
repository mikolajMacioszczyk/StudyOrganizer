using System;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizeDLLTests
{
    [TestFixture]
    public class UpdateSchoolTaskTests
    {
        private ConnectionToDb dbConnection;
        private UpdatesTransmitter _transmitter;
        [SetUp]
        public void SetUp()
        {
            dbConnection = new ConnectionToDb();
            _transmitter = new UpdatesTransmitter(dbConnection);
        }
        
        [Test]
        public void UpdateTaskTittleWithNullNoChangeExpected()
        {
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            _transmitter.UpdateTaskTittle(taskToUpdate, null);
            var taskAfterUpdates = dbConnection.GetSchoolTask("TestTitle");
            Assert.That(taskAfterUpdates.Title, Is.EqualTo("TestTitle"));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
        }
        
        [Test]
        public void UpdateTaskTittleWithEmptyStringNoChangeExpected()
        {
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            _transmitter.UpdateTaskTittle(taskToUpdate, "");
            var taskAfterUpdates = dbConnection.GetSchoolTask("TestTitle");
            Assert.That(taskAfterUpdates.Title, Is.EqualTo("TestTitle"));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
        }
        
        [Test]
        public void UpdateTaskTittleWithAlreadyExistingInDataBaseTittleNoChangeExpected()
        {    
            dbConnection.InsertSchoolTask(0,"AlreadyExisting", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            var otherTask = dbConnection.GetSchoolTask("AlreadyExisting");
            _transmitter.UpdateTaskTittle(taskToUpdate, "AlreadyExisting");
            var taskAfterUpdates = dbConnection.GetSchoolTask("TestTitle");
            Assert.That(taskAfterUpdates.Title, Is.EqualTo("TestTitle"));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
            dbConnection.DeleteSchoolTask(otherTask.Id);
        }
        
        [Test]
        public void UpdateTaskTittleWithValidTittleChangeExpected()
        {    
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            _transmitter.UpdateTaskTittle(taskToUpdate, "ValidTitle");
            var taskAfterUpdates = dbConnection.GetSchoolTask("ValidTitle");
            Assert.That(taskAfterUpdates.Title, Is.EqualTo("ValidTitle"));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
        }
        
        [Test]
        public void UpdateTaskDescriptionWithNullNoChangeExpected()
        {
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            _transmitter.UpdateTaskDescription(taskToUpdate, null);
            var taskAfterUpdates = dbConnection.GetSchoolTask("TestTitle");
            Assert.That(taskAfterUpdates.Description, Is.EqualTo("TestDescription"));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
        }
        
        [Test]
        public void UpdateTaskDescriptionWithEmptyStringNoChangeExpected()
        {
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            _transmitter.UpdateTaskDescription(taskToUpdate, "");
            var taskAfterUpdates = dbConnection.GetSchoolTask("TestTitle");
            Assert.That(taskAfterUpdates.Description, Is.EqualTo("TestDescription"));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
        }
        
        [Test]
        public void UpdateTaskDescriptionWithValidDescriptionChangeExpected()    
        {    
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            _transmitter.UpdateTaskDescription(taskToUpdate, "ValidDescription");
            var taskAfterUpdates = dbConnection.GetSchoolTask("TestTitle");
            Assert.That(taskAfterUpdates.Description, Is.EqualTo("ValidDescription"));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
        }
        
        [Test]
        public void UpdateTaskGroupChangeExpected()    
        {        
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            _transmitter.UpdateTaskGroup(taskToUpdate, TaskGroup.Planned);
            var taskAfterUpdates = dbConnection.GetSchoolTask("TestTitle");
            Assert.That(taskAfterUpdates.TaskGroup, Is.EqualTo(TaskGroup.Planned));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
        }
        
        [Test]
        public void UpdateTaskAwardedChangeExpected()    
        {        
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            _transmitter.UpdateTaskAward(taskToUpdate, true);
            var taskAfterUpdates = dbConnection.GetSchoolTask("TestTitle");
            Assert.That(taskAfterUpdates.IsAwarded, Is.EqualTo(true));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
        }
        
        [Test]
        public void UpdateTaskDeadlineChangeExpected()    
        {
            dbConnection.InsertSchoolTask(0,"TestTitle", "TestDescription", TaskGroup.Actual, false, DateTime.Now);
            var taskToUpdate = dbConnection.GetSchoolTask("TestTitle");
            _transmitter.UpdateTaskDeadline(taskToUpdate, DateTime.Now.AddDays(2));
            var taskAfterUpdates = dbConnection.GetSchoolTask("TestTitle");
            Assert.That(taskAfterUpdates.Deadline.Day, Is.EqualTo(DateTime.Now.AddDays(2).Day));
            dbConnection.DeleteSchoolTask(taskAfterUpdates.Id);
        }
    }
}