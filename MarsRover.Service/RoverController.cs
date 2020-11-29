using MarsRover.Service.CustomExceptions;
using MarsRover.Service.Enums;
using MarsRover.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Service
{
    public class RoverController : IRoverController
    {
        public Position Position { get; set; }
        public Plateau Plateau { get; set; }

        public void MoveForward()
        {
            ValidateCoordinate(Position.Coordinate);

            Coordinate nextCoordinate = new Coordinate
            {
                X = Position.Coordinate.X,
                Y = Position.Coordinate.Y
            };

            switch (Position.DirectionType)
            {
                case DirectionType.N:
                    nextCoordinate.Y++;
                    break;
                case DirectionType.S:
                    nextCoordinate.Y--;
                    break;
                case DirectionType.E:
                    nextCoordinate.X++;
                    break;
                case DirectionType.W:
                    nextCoordinate.X--;
                    break;
                default:
                    break;
            }

            ValidateCoordinate(nextCoordinate);
            Position.Coordinate = nextCoordinate;
        }

        private void ValidateCoordinate(Coordinate coordinate)
        {
            if (!(coordinate.X >= Plateau.LowerLefttCoordinate.X &&
                coordinate.X <= Plateau.UpperRightCoordinate.X &&
                coordinate.Y >= Plateau.LowerLefttCoordinate.Y &&
                coordinate.Y <= Plateau.UpperRightCoordinate.Y))
            {
                throw new RoverOutOfPlateauException();
            }
        }

        public void TurnLeft()
        {
            switch (Position.DirectionType)
            {
                case DirectionType.N:
                    Position.DirectionType = DirectionType.W;
                    break;
                case DirectionType.S:
                    Position.DirectionType = DirectionType.E;
                    break;
                case DirectionType.E:
                    Position.DirectionType = DirectionType.N;
                    break;
                case DirectionType.W:
                    Position.DirectionType = DirectionType.S;
                    break;
            }
        }

        public void TurnRight()
        {
            switch (Position.DirectionType)
            {
                case DirectionType.N:
                    Position.DirectionType = DirectionType.E;
                    break;
                case DirectionType.S:
                    Position.DirectionType = DirectionType.W;
                    break;
                case DirectionType.E:
                    Position.DirectionType = DirectionType.S;
                    break;
                case DirectionType.W:
                    Position.DirectionType = DirectionType.N;
                    break;
            }
        }

        public void ExecuteNavigationPlan(Plateau plateau, NavigationPlan navigationPlan)
        {
            this.Plateau = plateau;
            this.Position = navigationPlan.InitialPosition;

            foreach (InstructionType instructionType in navigationPlan.InstructionTypeSequence)
            {
                switch (instructionType)
                {
                    case InstructionType.M:
                        this.MoveForward();
                        break;
                    case InstructionType.L:
                        this.TurnLeft();
                        break;
                    case InstructionType.R:
                        this.TurnRight();
                        break;
                }
            }
        }
    }
}
