using MarsRover.Service.Enums;
using System.Collections.Generic;

namespace MarsRover.Service.Models
{
    public class NavigationPlan
    {
        public Position InitialPosition { get; set; }
        public List<InstructionType> InstructionTypeSequence { get; set; }
    }
}
