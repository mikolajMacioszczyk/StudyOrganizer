using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;

namespace StudyOrganizeDLLTests
{
    class ValidatorTest
    {
        [SetUp]
        public void BeforeEach()
        {

        }

        [Test]
        public void PasswordCorrectTest()
        {
            Validator.PasswordValidation("Pa55word");
        }

        [Test]
        public void PasswordWithoutNumberTest()
        {
            Assert.Throws<InvalidInputException>(() => { Validator.PasswordValidation("Password"); });

        }

        [Test]
        public void PasswordWithoutUppercaseTest()
        {
            Assert.Throws<InvalidInputException>(() => { Validator.PasswordValidation("pa55word"); });
        }

        [Test]
        public void PasswordWithSpaceTest()
        {
            Assert.Throws<InvalidInputException>(() => { Validator.PasswordValidation("Pa55w ord"); });
        }

        [Test]
        public void PasswordWithSpaceInTheEndTest()
        {
            Assert.Throws<InvalidInputException>(() => { Validator.PasswordValidation("pa55word "); });
        }
    }
}
