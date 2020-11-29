using MarsRover.Service.Models;

namespace MarsRover.Service
{
    public interface IMarsRoverService
    {
        /// <summary>
        /// Returns the last positions of the rovers as a string output by taking a string input in the specified format.
        /// </summary>
        /// <param name="input">The string input corresponding to the specified format </param>
        /// <returns>The last positions of the rovers as a string output</returns>
        string ExecuteExplorationPlan(string input);
        /// <summary>
        /// Returns the last positions of the rovers as a FinalStatus object by taking a ExplorationPlan object in the specified format.
        /// </summary>
        /// <param name="explorationPlan"></param>
        /// <returns>The last positions of the rovers as a FinalStatus object</returns>
        FinalStatus ExecuteExplorationPlan(ExplorationPlan explorationPlan);
    }    
}
