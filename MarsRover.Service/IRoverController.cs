using MarsRover.Service.Models;

namespace MarsRover.Service
{
    public interface IRoverController
    {
        /// <summary>
        /// The current position
        /// </summary>
        Position Position { get; set; }
        /// <summary>
        /// The surrounding terrain
        /// </summary>
        Plateau Plateau { get; set; }


        /// <summary>
        /// Move forward one grid point
        /// </summary>
        void MoveForward();
        /// <summary>
        /// Spin 90 degrees left
        /// </summary>
        void TurnLeft();
        /// <summary>
        /// Spin 90 degrees right
        /// </summary>
        void TurnRight();
        /// <summary>
        /// Move the rover according to the given initial position and instructions
        /// </summary>
        void ExecuteNavigationPlan(Plateau plateau, NavigationPlan navigationPlan);
    }
}
