using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizeDLLTests
{
    [TestFixture]
    public class SavingUserTests
    {
        private static string File = "SavingUserTestsFile.bin";

        [SetUp]
        public void BeforeEach()
        {
            UserDataBase.ClearFile(File);
        }
        
        [Test]
        public void SerializeUserByPlaceholder()
        {
            User user = new User("Name","Study",1,"Login");
            UserDataBase.SaveUser(File,user);
        }
        
        [Test]
        public void DeserializeUserByPlaceholder()
        {
            User user = new User("Name","Study",1,"Login");
            UserDataBase.SaveUser(File,user);
            var afterSerialization = UserDataBase.GetUserFromFile("Login" + File);
            Assert.AreEqual(user,afterSerialization);
        }
        
        [Test]
        public void DeserializeUserWithWrongPath()
        {
            Assert.Throws<FileNotFoundException>((() => UserDataBase.GetUserFromFile("WrongPath")));
        }
        
        [Test]
        public void SerializeNull()
        {
            Assert.Throws<NullReferenceException>((() => UserDataBase.SaveUser(File,null)));
        }
    }
}