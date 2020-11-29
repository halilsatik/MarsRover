using MarsRover.Service.CustomExceptions;
using MarsRover.Service.Models;
using Xunit;

namespace MarsRover.Service.Tests
{
    public class ExplorationPlanDeserializerTest
    {
        private readonly IExplorationPlanDeserializer _explorationPlanDeserializer;

        public ExplorationPlanDeserializerTest()
        {
            _explorationPlanDeserializer = new ExplorationPlanDeserializer();
        }

        [Fact]
        public void ExplorationPlanDeserializer_DeserializeExplorationPlanWithInvalidArgument_ThrowsInputInvalidLineCountException()
        {
            string testInput = "5 5\n" +
                               "1 2 N\n" +
                               "LMLMLMLMM\n" +
                               "3 3 E\n";
                               // the series of instructions of the second rover is missing.

            Assert.Throws<InputInvalidLineCountException>(() => { ExplorationPlan explorationPlan = _explorationPlanDeserializer.DeserializeExplorationPlan(testInput); });
        }

        [Fact]
        public void ExplorationPlanDeserializer_DeserializeExplorationPlanWithInvalidArgument_ThrowsInputInvalidPlateauLineException()
        {
            string testInput = "5 E\n" + // the plateau Y coordinate is invalid.
                               "1 2 N\n" +
                               "LMLMLMLMM\n" +
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";

            Assert.Throws<InputInvalidPlateauLineException>(() => { ExplorationPlan explorationPlan = _explorationPlanDeserializer.DeserializeExplorationPlan(testInput); });
        }

        [Fact]
        public void ExplorationPlanDeserializer_DeserializeExplorationPlanWithInvalidArgument_ThrowsInputInvalidRoverInitialPositionLineException()
        {
            string testInput = "5 5\n" + 
                               "1 2 X\n" + // the direction of the first rover is invalid.
                               "LMLMLMLMM\n" +
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";

            Assert.Throws<InputInvalidRoverInitialPositionLineException>(() => { ExplorationPlan explorationPlan = _explorationPlanDeserializer.DeserializeExplorationPlan(testInput); });
        }

        [Fact]
        public void ExplorationPlanDeserializer_DeserializeExplorationPlanWithInvalidArgument_ThrowsInputInvalidInstructionTypeSequenceLineException()
        {
            string testInput = "5 5\n" +
                               "1 2 N\n" + 
                               "LMLMLMLMM\n" +
                               "3 3 E\n" +
                               "MMRM MRMRRM\n"; // the series of instructions of the second rover is invalid.

            Assert.Throws<InputInvalidInstructionTypeSequenceLineException>(() => { ExplorationPlan explorationPlan = _explorationPlanDeserializer.DeserializeExplorationPlan(testInput); });
        }        
    }
}
