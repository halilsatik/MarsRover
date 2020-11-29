using System.Collections.Generic;

namespace MarsRover.Service.Models
{
    public class ExplorationPlan
    {
        public Plateau Plateau { get; set; }
        public List<NavigationPlan> RoverNavigationPlanList { get; set; }
    }
}
