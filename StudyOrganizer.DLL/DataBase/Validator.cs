using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyOrganizer.DLL.Exceptions;

namespace StudyOrganizer.DLL.DataBase
{
    public static class Validator
    {
        public static bool PasswordValidation(string password)
        {
            if (string.IsNullOrEmpty(password) || !password.Any(char.IsUpper) || !password.Any(char.IsLower)
            || !password.Any(char.IsDigit) || password.Any(char.IsWhiteSpace))
            {
                throw new InvalidInputException("Password should contains uppercase letter, lowercase letter, digit and shouldn't contains white space");
            }
            return true;
        }
    }
}
