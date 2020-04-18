using System;
using System.Collections.Generic;
using System.Text;

namespace StudyOrganizer.DLL.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {
        }

        public InvalidInputException()
        {
            
        }
    }
}
