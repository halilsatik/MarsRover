using System;
using System.Text;

namespace MarsRover.Service.Models
{
    public class Plateau
    {
        /// <summary>
        /// the lower-left coordinates are assumed to be 0,0.
        /// </summary>
        public Coordinate LowerLefttCoordinate { get; set; } = new Coordinate { X = 0, Y = 0 };
        public Coordinate UpperRightCoordinate { get; set; }
    }
}
