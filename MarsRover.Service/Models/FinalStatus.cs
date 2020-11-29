using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover.Service.Models
{
    public class FinalStatus : IEquatable<FinalStatus>
    {
        public List<Position> FinalRoverPositionList { get; set; }

        public bool Equals(FinalStatus otherFinalStatus)
        {
            if (FinalRoverPositionList.Count == otherFinalStatus.FinalRoverPositionList.Count)
            {
                for (int index = 0; index < FinalRoverPositionList.Count; index++)
                {
                    if (FinalRoverPositionList[index].DirectionType != otherFinalStatus.FinalRoverPositionList[index].DirectionType ||
                        FinalRoverPositionList[index].Coordinate.X != otherFinalStatus.FinalRoverPositionList[index].Coordinate.X ||
                        FinalRoverPositionList[index].Coordinate.Y != otherFinalStatus.FinalRoverPositionList[index].Coordinate.Y)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
