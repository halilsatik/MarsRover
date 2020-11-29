using MarsRover.Service.Models;

namespace MarsRover.Service
{
    public interface IFinalStatusSerializer
    {
        /// <summary>
        /// Serialize a FinalStatus object to string in the specified format.
        /// </summary>
        /// <param name="finalStatus"></param>
        /// <returns>The last positions of the rovers as a string output</returns>
        string SerializeFinalStatus(FinalStatus finalStatus);
    }
}