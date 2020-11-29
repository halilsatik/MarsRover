using System;

namespace MarsRover.Service.CustomExceptions
{
    public class RoverOutOfPlateauException : Exception
    {
        public RoverOutOfPlateauException()
        {

        }

        public RoverOutOfPlateauException(string message) : base(message)
        {

        }
    }
}
