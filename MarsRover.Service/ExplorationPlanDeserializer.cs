using MarsRover.Service.CustomExceptions;
using MarsRover.Service.Enums;
using MarsRover.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover.Service
{
    public class ExplorationPlanDeserializer : IExplorationPlanDeserializer
    {
        private const int MinNumberOfInputLines = 3;
        private const int NumberOfInputLinesForPlateau = 1;
        private const int NumberOfInputLinesForEachRoverNavigationPlan = 2;
        private const int NumberOfTokensForPlateauInputLine = 2;
        private const int NumberOfTokensForEachRoverInitialPositionInputLine = 3;

        public ExplorationPlan DeserializeExplorationPlan(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException();
            }

            List<string> inputLineList = new List<string>(input.Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));

            if (!(inputLineList.Count >= MinNumberOfInputLines && ((inputLineList.Count - NumberOfInputLinesForPlateau) % NumberOfInputLinesForEachRoverNavigationPlan == 0)))
            {
                throw new InputInvalidLineCountException();
            }

            Plateau plateau = DeserializePlateauInputLine(inputLineList);
            List<NavigationPlan> roverNavigationPlanList = DeserializeRoverNavigationPlanInputLineList(inputLineList);

            ExplorationPlan explorationPlan = new ExplorationPlan
            {
                Plateau = plateau,
                RoverNavigationPlanList = roverNavigationPlanList,
            };
            return explorationPlan;
        }

        public Plateau DeserializePlateauInputLine(List<string> inputLineList)
        {
            string plateauInputLine = inputLineList[0];
            inputLineList.RemoveAt(0);

            string[] plateauCoordinates = plateauInputLine.Trim().Split(' ');

            if (plateauCoordinates.Length != NumberOfTokensForPlateauInputLine || !int.TryParse(plateauCoordinates[0], out int plateauCoordinateX) || !int.TryParse(plateauCoordinates[1], out int plateauCoordinateY))
            {
                throw new InputInvalidPlateauLineException();
            }

            Plateau plateau = new Plateau { UpperRightCoordinate = new Coordinate { X = plateauCoordinateX, Y = plateauCoordinateY } };
            return plateau;
        }

        public List<NavigationPlan> DeserializeRoverNavigationPlanInputLineList(List<string> inputLineList)
        {
            List<NavigationPlan> roverNavigationPlanList = new List<NavigationPlan>();

            for (int lineIndex = 0; lineIndex < inputLineList.Count; lineIndex += 2)
            {
                string roverInitialPositionInputLine = inputLineList[lineIndex];
                Position roverInitialPosition = DeserializeRoverInitialPositionInputLine(roverInitialPositionInputLine);

                string roverInstructionTypeSequenceInputLine = inputLineList[lineIndex + 1];
                List<InstructionType> roverInstructionTypeList = DeserializeRoverInstructionTypeSequenceInputLine(roverInstructionTypeSequenceInputLine);

                NavigationPlan roverNavigationPlan = new NavigationPlan
                {
                    InitialPosition = roverInitialPosition,
                    InstructionTypeSequence = roverInstructionTypeList
                };

                roverNavigationPlanList.Add(roverNavigationPlan);
            }

            return roverNavigationPlanList;
        }

        public Position DeserializeRoverInitialPositionInputLine(string roverInitialPositionInputLine)
        {
            string[] roverInitialPositionParameters = roverInitialPositionInputLine.Trim().Split(' ');

            if (roverInitialPositionParameters.Length != NumberOfTokensForEachRoverInitialPositionInputLine ||
                !int.TryParse(roverInitialPositionParameters[0], out int roverInitialPositionCoordinateX) ||
                !int.TryParse(roverInitialPositionParameters[1], out int roverInitialPositionCoordinateY) ||
                !Enum.TryParse(roverInitialPositionParameters[2], out DirectionType directionType))
            {
                throw new InputInvalidRoverInitialPositionLineException();
            }

            Position roverInitialPosition = new Position { Coordinate = new Coordinate { X = roverInitialPositionCoordinateX, Y = roverInitialPositionCoordinateY }, DirectionType = directionType };
            return roverInitialPosition;
        }

        public List<InstructionType> DeserializeRoverInstructionTypeSequenceInputLine(string roverInstructionTypeSequenceInputLine)
        {
            return roverInstructionTypeSequenceInputLine
                                .Select(instructionTypeChar =>
                                Enum.TryParse(instructionTypeChar.ToString(), out InstructionType instructionType) ?
                                instructionType :
                                throw new InputInvalidInstructionTypeSequenceLineException()
                                ).ToList();
        }
    }
}
