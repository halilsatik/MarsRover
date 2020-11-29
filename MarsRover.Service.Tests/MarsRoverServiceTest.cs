using MarsRover.Service.CustomExceptions;
using MarsRover.Service.Enums;
using MarsRover.Service.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRover.Service.Tests
{
    public class MarsRoverServiceTest
    {
        private readonly IExplorationPlanDeserializer _explorationPlanDeserializer;
        private readonly IFinalStatusSerializer _finalStatusSerializer;
        private readonly IMarsRoverService _marsRoverService;

        public MarsRoverServiceTest()
        {
            _explorationPlanDeserializer = new ExplorationPlanDeserializer();
            _finalStatusSerializer = new FinalStatusSerializer();
            _marsRoverService = new MarsRoverService(_explorationPlanDeserializer, _finalStatusSerializer);
        }

        [Fact]
        public void MarsRoverService_ExecuteExplorationPlan_ValidStringInput()
        {
            string testInput = "5 5\n" +
                               "1 2 N\n" +
                               "LMLMLMLMM\n" +
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";

            string expectedOutput = "1 3 N\n" +
                                    "5 1 E\n";

            string actualOutput = _marsRoverService.ExecuteExplorationPlan(testInput);

            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void MarsRoverService_ExecuteExplorationPlan_ValidExplorationPlanInput()
        {
            ExplorationPlan explorationPlan = new ExplorationPlan
            {
                Plateau = new Plateau
                {
                    LowerLefttCoordinate = new Coordinate { X = 0, Y = 0 },
                    UpperRightCoordinate = new Coordinate { X = 5, Y = 5 }
                },
                RoverNavigationPlanList = new List<NavigationPlan>
                {
                    new NavigationPlan
                    {
                        InitialPosition = new Position  { Coordinate = new Coordinate { X = 1, Y = 2 }, DirectionType = DirectionType.N },
                        InstructionTypeSequence = new List<InstructionType> 
                        { 
                            InstructionType.L, 
                            InstructionType.M,
                            InstructionType.L,
                            InstructionType.M,
                            InstructionType.L,
                            InstructionType.M,
                            InstructionType.L,
                            InstructionType.M,
                            InstructionType.M,
                        }
                    },
                    new NavigationPlan
                    {
                        InitialPosition = new Position  { Coordinate = new Coordinate { X = 3, Y = 3 }, DirectionType = DirectionType.E },
                        InstructionTypeSequence = new List<InstructionType>
                        {
                            InstructionType.M,
                            InstructionType.M,
                            InstructionType.R,
                            InstructionType.M,
                            InstructionType.M,
                            InstructionType.R,
                            InstructionType.M,
                            InstructionType.R,
                            InstructionType.R,
                            InstructionType.M,
                        }
                    }
                }
            };

            FinalStatus expectedFinalStatus = new FinalStatus
            {
                FinalRoverPositionList = new List<Position>
                {
                    new Position { Coordinate = new Coordinate { X = 1, Y = 3 }, DirectionType = DirectionType.N },
                    new Position { Coordinate = new Coordinate { X = 5, Y = 1 }, DirectionType = DirectionType.E }
                }
            };

            IMarsRoverService marsRoverService = new MarsRoverService();
            FinalStatus actualFinalStatus = marsRoverService.ExecuteExplorationPlan(explorationPlan);

            Assert.Equal(expectedFinalStatus, actualFinalStatus);
        }

        [Fact]
        public void MarsRoverService_ExecuteExplorationPlanWithInvalidArgument_ThrowsRoverOutOfPlateauException_BySeriesOfInstructions()
        {
            string testInput = "5 5\n" +
                               "1 2 N\n" +
                               "LMLMLMLMMMMMM\n" + // the first rover is out of the plateau according to the series of instructions
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";
                                  
            Assert.Throws<RoverOutOfPlateauException>(() => { string actualOutput = _marsRoverService.ExecuteExplorationPlan(testInput); });
        }

        [Fact]
        public void MarsRoverService_ExecuteExplorationPlanWithInvalidArgument_ThrowsRoverOutOfPlateauException_ByRoverInitialPosition()
        {
            string testInput = "5 5\n" +
                               "1 2 N\n" +
                               "LMLMLMLMM\n" +
                               "7 3 E\n" + // the second rover is out of the plateau according to its initial position
                               "MMRMMRMRRM\n";

            Assert.Throws<RoverOutOfPlateauException>(() => { string actualOutput = _marsRoverService.ExecuteExplorationPlan(testInput); });
        }

        [Fact]
        public void MarsRoverService_ExecuteExplorationPlanWithMissingConstructorArgumentExplorationPlanDeserializer_ThrowsArgumentNullException()
        {
            string testInput = "5 5\n" +
                               "1 2 N\n" +
                               "LMLMLMLMM\n" +
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";

            IMarsRoverService marsRoverService = new MarsRoverService(null, _finalStatusSerializer); // the missing parameter is explorationPlanDeserializer
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(() => { string actualOutput = marsRoverService.ExecuteExplorationPlan(testInput); });

            Assert.Equal("explorationPlanDeserializer", argumentNullException.ParamName);
        }

        [Fact]
        public void MarsRoverService_ExecuteExplorationPlanWithMissingConstructorArgumentFinalStatusSerializer_ThrowsArgumentNullException()
        {
            string testInput = "5 5\n" +
                               "1 2 N\n" +
                               "LMLMLMLMM\n" +
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";

            IMarsRoverService marsRoverService = new MarsRoverService(_explorationPlanDeserializer, null); // the missing parameter is finalStatusSerializer
            ArgumentNullException argumentNullException = Assert.Throws<ArgumentNullException>(() => { string actualOutput = marsRoverService.ExecuteExplorationPlan(testInput); });

            Assert.Equal("finalStatusSerializer", argumentNullException.ParamName);
        }
    }
}
