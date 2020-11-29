using MarsRover.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Service
{
    public class FinalStatusSerializer : IFinalStatusSerializer
    {
        public string SerializeFinalStatus(FinalStatus finalStatus)
        {
            string output = "";

            foreach (Position finalRoverPosition in finalStatus.FinalRoverPositionList)
            {
                output += $"{finalRoverPosition.Coordinate.X} {finalRoverPosition.Coordinate.Y} {finalRoverPosition.DirectionType}\n";
            }

            return output;
        }
    }
}
