using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Research.Rewards
{
    public class ChangeResearchSlotAmountReward : CompletionReward
    {
        [ModelProperty]
        public int Amount;

        public override string Description => "+" + Amount + " research slots.";

        public override void ApplyReward()
        {
            ResearchController.Instance.SetMaxResearchSlots(ResearchController.Instance.MaxResearchSlots + Amount);
        }
    }
}
