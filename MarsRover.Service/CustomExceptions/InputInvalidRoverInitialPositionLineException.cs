using System;

namespace MarsRover.Service.CustomExceptions
{
    public class InputInvalidRoverInitialPositionLineException : Exception
    {
        public InputInvalidRoverInitialPositionLineException()
        {

        }

        public InputInvalidRoverInitialPositionLineException(string message) : base(message)
        {

        }
    }
}
