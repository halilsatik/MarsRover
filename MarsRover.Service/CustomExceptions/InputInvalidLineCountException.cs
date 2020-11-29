using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Service.CustomExceptions
{
    public class InputInvalidLineCountException : Exception
    {
        public InputInvalidLineCountException()
        {

        }

        public InputInvalidLineCountException(string message) : base(message)
        {

        }
    }
}
