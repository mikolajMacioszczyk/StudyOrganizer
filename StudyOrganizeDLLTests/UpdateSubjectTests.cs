using System;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizeDLLTests
{
    [TestFixture]
    public class UpdateSubjectTests
    {
        private UpdatesTransmitter _transmitter;
        private ConnectionToDb _dbConnection;

        [SetUp]
        public void SetUp()
        {
            _dbConnection = new ConnectionToDb();
            _transmitter = new UpdatesTransmitter(_dbConnection);
        }
        
        [Test]
        public void UpdateSubjectNameWithNullNoChangeExpected()
        {
            _dbConnection.InsertSubject(0, "TestSubject", SubjectTypes.Cwiczenia, "Monday", 5);
            Subject beforeUpdating = _dbConnection.GetSubject("TestSubject");
            _transmitter.UpdateSubjectName(beforeUpdating,null);
            Subject afterUpdating = _dbConnection.GetSubject("TestSubject");
            Assert.NotNull(afterUpdating);
            _dbConnection.DeleteSubject(afterUpdating.Id);
        }
        
        [Test]
        public void UpdateSubjectNameWithEmptyStringNoChangeExpected()
        {
            _dbConnection.InsertSubject(0, "TestSubject", SubjectTypes.Cwiczenia, "Monday", 5);
            Subject beforeUpdating = _dbConnection.GetSubject("TestSubject");
            _transmitter.UpdateSubjectName(beforeUpdating,"");
            Subject afterUpdating = _dbConnection.GetSubject("TestSubject");
            Assert.NotNull(afterUpdating);
            _dbConnection.DeleteSubject(afterUpdating.Id);
        }
        
        [Test]
        public void UpdateSubjectNameWithNameAlreadyInDatabaseNoChangeExpected()
        {
            _dbConnection.InsertSubject(0, "TestSubject", SubjectTypes.Cwiczenia, "Monday", 5);
            _dbConnection.InsertSubject(0, "OtherSubject", SubjectTypes.Cwiczenia, "Tuesday", 5);
            Subject beforeUpdating = _dbConnection.GetSubject("TestSubject");
            Subject otherSubject = _dbConnection.GetSubject("OtherSubject");
            Assert.Throws<InvalidOperationException>(() => _transmitter.UpdateSubjectName(beforeUpdating,"OtherSubject"));
            Subject afterUpdating = _dbConnection.GetSubject("TestSubject");
            Assert.NotNull(afterUpdating);
            _dbConnection.DeleteSubject(afterUpdating.Id);
            _dbConnection.DeleteSubject(otherSubject.Id);
        }
        
        [Test]
        public void UpdateSubjectNameWithValidNameChangeExpected()
        {
            _dbConnection.InsertSubject(0, "TestSubject", SubjectTypes.Cwiczenia, "Monday", 5);
            Subject beforeUpdating = _dbConnection.GetSubject("TestSubject");
            _transmitter.UpdateSubjectName(beforeUpdating,"ValidName");
            Subject afterUpdating = _dbConnection.GetSubject("ValidName");
            Assert.Throws<InvalidOperationException>(() =>_dbConnection.GetSubject("TestSubject"));
            Assert.NotNull(afterUpdating);
            _dbConnection.DeleteSubject(afterUpdating.Id);
        }
        
        [Test]
        public void UpdateSubjectTypeChangeExpected()
        {
            _dbConnection.InsertSubject(0, "TestSubject", SubjectTypes.Cwiczenia, "Monday", 5);
            Subject beforeUpdating = _dbConnection.GetSubject("TestSubject");
            _transmitter.UpdateSubjectType(beforeUpdating,SubjectTypes.Wyklad);
            Subject afterUpdating = _dbConnection.GetSubject("TestSubject");
            Assert.AreEqual(SubjectTypes.Wyklad, afterUpdating.Type);
            _dbConnection.DeleteSubject(afterUpdating.Id);
        }
        
        [Test]
        public void UpdateSubjectDayChangeExpected()
        {
            _dbConnection.InsertSubject(0, "TestSubject", SubjectTypes.Cwiczenia, "Monday", 5);
            Subject beforeUpdating = _dbConnection.GetSubject("TestSubject");
            _transmitter.UpdateSubjectDay(beforeUpdating,DayOfWeek.Friday);
            Subject afterUpdating = _dbConnection.GetSubject("TestSubject");
            Assert.AreEqual(DayOfWeek.Friday, afterUpdating.Day);
            _dbConnection.DeleteSubject(afterUpdating.Id);
        }
        
        [Test]
        public void UpdateSubjectHourChangeExpected()
        {
            _dbConnection.InsertSubject(0, "TestSubject", SubjectTypes.Cwiczenia, "Monday", 5);
            Subject beforeUpdating = _dbConnection.GetSubject("TestSubject");
            _transmitter.UpdateSubjectHour(beforeUpdating,10);
            Subject afterUpdating = _dbConnection.GetSubject("TestSubject");
            Assert.AreEqual(10, afterUpdating.Hour);
            _dbConnection.DeleteSubject(afterUpdating.Id);
        }
    }
}