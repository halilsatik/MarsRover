# Mars Rover Case Study

This repository hosts a solution that contains the source (.NET Standart 2.1) and test (.NET Core 3.1) project.

## Problem
A squad of robotic rovers are to be landed by NASA on a plateau on Mars. This plateau, which is curiously rectangular, must be navigated by the rovers so that their on board cameras can get a complete view of the surrounding terrain to send back to Earth.
A rover's position and location is represented by a combination of x and y co-ordinates and a letter representing one of the four cardinal compass points. The plateau is divided up into a grid to simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom left corner and facing North.
In order to control a rover, NASA sends a simple string of letters. The possible letters are 'L', 'R' and 'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its current spot. 'M' means move forward one grid point, and maintain the same heading.
Assume that the square directly North from (x, y) is (x, y+1).

## Input: 
The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are assumed to be 0,0.
The rest of the input is information pertaining to the rovers that have been deployed. Each rover has two lines of input. The first line gives the rover's position, and the second line is a series of instructions telling the rover how to explore the plateau.
The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.
Each rover will be finished sequentially, which means that the second rover won't start to move until the first one has finished moving.

## Output: 
The output for each rover should be its final co-ordinates and heading.

## Input and Output
### Test Input:
```bash
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
```  
### Expected Output:
```bash
1 3 N
5 1 E
```

## Example Usage 1 (Input As String)
```csharp
string testInput = "5 5\n" +
                   "1 2 N\n" +
                   "LMLMLMLMM\n" +
                   "3 3 E\n" +
                   "MMRMMRMRRM\n";
                   
IExplorationPlanDeserializer explorationPlanDeserializer = new ExplorationPlanDeserializer();
IFinalStatusSerializer finalStatusSerializer = new FinalStatusSerializer();
IMarsRoverService marsRoverService = new MarsRoverService(_explorationPlanDeserializer, _finalStatusSerializer);

string output = marsRoverService.ExecuteExplorationPlan(testInput);
```

## Example Usage 2 (Input As ExplorationPlan Object)
```csharp
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

IMarsRoverService marsRoverService = new MarsRoverService();

FinalStatus finalStatus = marsRoverService.ExecuteExplorationPlan(explorationPlan);
```

## Test
This project's tests require having "dotnet" on your path.  
Run the following command inside this directory: "MarsRover\MarsRover.Service.Tests".
```
dotnet test
```