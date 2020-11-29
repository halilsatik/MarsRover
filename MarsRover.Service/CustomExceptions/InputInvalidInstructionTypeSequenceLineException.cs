using System;

namespace MarsRover.Service.CustomExceptions
{
    public class InputInvalidInstructionTypeSequenceLineException : Exception
    {
        public InputInvalidInstructionTypeSequenceLineException()
        {

        }

        public InputInvalidInstructionTypeSequenceLineException(string message) : base(message)
        {

        }
    }
}
