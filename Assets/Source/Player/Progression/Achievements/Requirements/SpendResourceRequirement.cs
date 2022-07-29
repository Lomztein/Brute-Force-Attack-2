using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class SpendResourceRequirement : AchievementRequirement
    {
        [ModelAssetReference]
        public Resource Resource;
        [ModelProperty]
        public int TargetSpent;
        [ModelProperty]
        public bool AllowGreater;
        [ModelProperty]
        public bool AllowLesser;
        [ModelProperty]
        public bool CrossGames;
        [ModelProperty]
        public bool SinglePurchase;

        private int _spent;

        protected override bool MeetsRequirements()
        {
            return _spent == TargetSpent ||
                (AllowGreater && _spent > TargetSpent) ||
                (AllowLesser && _spent < TargetSpent);
        }

        public override void End()
        {
            Facade.Battlefield.Player.OnResourceChanged -= Player_OnResourceChanged;
            if (!CrossGames)
            {
                Facade.Battlefield.OnSceneUnloaded -= Battlefield_OnSceneUnloaded;
            }
        }

        public override void Init()
        {
            Facade.Battlefield.Player.OnResourceChanged += Player_OnResourceChanged;
            if (!CrossGames)
            {
                Facade.Battlefield.OnSceneUnloaded += Battlefield_OnSceneUnloaded;
            }
        }

        private void Battlefield_OnSceneUnloaded()
        {
            _spent = 0;
        }

        private void Player_OnResourceChanged(Resource arg1, int before, int after)
        {
            if (arg1.Identifier == Resource.Identifier)
            {
                int diff = before - after;
                if (diff > 0)
                {
                    _spent += diff;
                    CheckRequirements();
                }
                if (SinglePurchase)
                {
                    _spent = 0;
                }
            }
        }
    }
}
