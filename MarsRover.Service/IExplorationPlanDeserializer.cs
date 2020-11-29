using MarsRover.Service.Enums;
using MarsRover.Service.Models;
using System.Collections.Generic;

namespace MarsRover.Service
{
    public interface IExplorationPlanDeserializer
    {
        /// <summary>
        /// Deserialize by taking a string input in the specified format.
        /// </summary>
        /// <param name="input">The string input corresponding to the specified format</param>
        /// <returns>A ExplorationPlan as object</returns>
        ExplorationPlan DeserializeExplorationPlan(string input);
        /// <summary>
        /// Extract the plateau information from the input lines
        /// </summary>
        /// <param name="inputLineList"></param>
        /// <returns></returns>
        Plateau DeserializePlateauInputLine(List<string> inputLineList);
        /// <summary>
        /// Extract the initial position of the rover from the input line
        /// </summary>
        /// <param name="roverInitialPositionInputLine"></param>
        /// <returns></returns>
        Position DeserializeRoverInitialPositionInputLine(string roverInitialPositionInputLine);
        /// <summary>
        /// Extract the series of instructions of the rover from the input line
        /// </summary>
        /// <param name="roverInstructionTypeSequenceInputLine"></param>
        /// <returns></returns>
        List<InstructionType> DeserializeRoverInstructionTypeSequenceInputLine(string roverInstructionTypeSequenceInputLine);
        /// <summary>
        /// Extract the initial position and the series of instructions of the rover from the input lines
        /// </summary>
        /// <param name="inputLineList"></param>
        /// <returns></returns>
        List<NavigationPlan> DeserializeRoverNavigationPlanInputLineList(List<string> inputLineList);
    }
}