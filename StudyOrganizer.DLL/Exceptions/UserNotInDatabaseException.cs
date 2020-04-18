using System;

namespace StudyOrganizer.DLL.Exceptions
{
    public class UserNotInDatabaseException : Exception
    {
        public UserNotInDatabaseException(string message = "") : base(message)
        {
            
        }
    }
}