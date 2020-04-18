﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;

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

            var expected = true;
            var result = UserDataBase.IsUserRegistered(FilePath, "Login", "Pa55word");
            Assert.AreEqual(expected,result);
        }

        [Test]
        public void IsUserRegisteredTestNull()
        {
            var expected = false;
            var result = UserDataBase.IsUserRegistered(FilePath, null, "Pa55word");
            Assert.AreEqual(expected, result);
        }
    }
}