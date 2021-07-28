using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Research;
using Lomztein.BFA2.Research.Rewards;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Player.Progression.Research.Rewards
{
    public class AddResearchReward : CompletionReward
    {
        [ModelAssetReference]
        public ResearchOption Research;
        [ModelProperty]
        public ResourceCost AddedCost;

        public override void ApplyReward()
        {
            ResearchOption research = UnityEngine.Object.Instantiate(Research);
            ResourceCost[] combined = new ResourceCost[] { research.ResourceCost, AddedCost };
            research.ResourceCost = combined.Sum().ToResourceCost();
            ResearchController.Instance.AddResearchOption(Research);
        }
    }
}
