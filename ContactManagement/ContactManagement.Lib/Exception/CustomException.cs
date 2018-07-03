using System;

namespace ContactManagement.Lib
{
    public class CustomException : Exception 
    {
        public CustomException(string errorMessage) : base(errorMessage)
        { }

    }
}
