using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class EarnResourceRequirement : AchievementRequirement
    {
        public override bool Binary => false;
        public override float Progression => (float)_earned / Amount;
        public override bool RequirementsMet => _earned >= Amount;

        private int _earned;
        [ModelProperty]
        public Resource Type;
        [ModelProperty]
        public int Amount;
        [ModelProperty]
        public bool CrossGames;

        public override void End()
        {
            Facade.Battlefield.Player.OnResourceChanged -= Player_OnResourceChanged;
            if (!CrossGames)
            {
                Facade.Battlefield.OnSceneLoaded -= Battlefield_OnSceneLoaded;
            }
        }

        public override void Init()
        {
            Facade.Battlefield.Player.OnResourceChanged += Player_OnResourceChanged;
            if (!CrossGames)
            {
                Facade.Battlefield.OnSceneLoaded += Battlefield_OnSceneLoaded;
            }
        }

        private void Battlefield_OnSceneLoaded()
        {
            _earned = 0;
            CheckRequirements();
        }

        private void Player_OnResourceChanged(Resource type, int arg2, int arg3)
        {
            if (type == Type)
            {
                int change = arg3 - arg2;
                if (change > 0)
                {
                    _earned += change;
                    CheckRequirements();
                }
            }
        }

        public override ValueModel SerializeProgress()
        {
            return new PrimitiveModel(_earned);
        }

        public override void DeserializeProgress(ValueModel source)
        {
            _earned = (source as PrimitiveModel).ToObject<int>();
        }
    }
}
