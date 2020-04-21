using System;

namespace StudyOrganizer.DLL.Exceptions
{
    public class LackOfDataException : Exception
    {
        public LackOfDataException(string Message) : base(Message)
        {
            
        }
    }
}