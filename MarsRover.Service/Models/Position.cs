using MarsRover.Service.Enums;

namespace MarsRover.Service.Models
{
    public class Position
    {
        public Coordinate Coordinate { get; set; }
        public DirectionType DirectionType { get; set; }
    }
}
