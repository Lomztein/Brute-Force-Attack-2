using Lomztein.BFA2.Scenes.Battlefield.Mutators;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Rewards
{
    public class AddMutatorReward : IAchievementReward
    {
        [ModelProperty]
        public Mutator Mutator;

        public void Apply()
        {
            Facade.MainMenu.Mutators.AddMutator(Mutator);
        }

        public void Remove()
        {
            Facade.MainMenu.Mutators.RemoveMutator(Mutator);
        }
    }
}
