using MarsRover.Service.Enums;
using MarsRover.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Service
{
    public class MarsRoverService : IMarsRoverService
    {
        private readonly IExplorationPlanDeserializer _explorationPlanDeserializer;
        private readonly IFinalStatusSerializer _finalStatusSerializer;

        public MarsRoverService()
        {

        }

        public MarsRoverService(IExplorationPlanDeserializer explorationPlanDeserializer, IFinalStatusSerializer finalStatusSerializer)
        {
            this._explorationPlanDeserializer = explorationPlanDeserializer;
            this._finalStatusSerializer = finalStatusSerializer;
        }

        public string ExecuteExplorationPlan(string input)
        {
            if (_explorationPlanDeserializer == null)
            {
                throw new ArgumentNullException("explorationPlanDeserializer");
            }

            if (_finalStatusSerializer == null)
            {
                throw new ArgumentNullException("finalStatusSerializer");
            }

            ExplorationPlan explorationPlan = _explorationPlanDeserializer.DeserializeExplorationPlan(input);

            FinalStatus finalStatus = this.ExecuteExplorationPlan(explorationPlan);

            string output = _finalStatusSerializer.SerializeFinalStatus(finalStatus);
            return output;
        }
                       
        public FinalStatus ExecuteExplorationPlan(ExplorationPlan explorationPlan)
        {
            List<IRoverController> roverControllerList = new List<IRoverController>();

            foreach (NavigationPlan roverNavigationPlan in explorationPlan.RoverNavigationPlanList)
            {
                IRoverController roverController = new RoverController();
                roverControllerList.Add(roverController);

                roverController.ExecuteNavigationPlan(explorationPlan.Plateau, roverNavigationPlan);
            }

            FinalStatus finalStatus = new FinalStatus
            {
                FinalRoverPositionList = roverControllerList.Select(roverController => roverController.Position).ToList()
            };

            return finalStatus;
        }
    }
}
