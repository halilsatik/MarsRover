using System;

namespace MarsRover.Service.CustomExceptions
{
    public class InputInvalidPlateauLineException : Exception
    {
        public InputInvalidPlateauLineException()
        {

        }

        public InputInvalidPlateauLineException(string message) : base(message)
        {

        }
    }
}
