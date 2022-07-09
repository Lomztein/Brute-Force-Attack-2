using System;
using System.Collections.Generic;
using System.Linq;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets
{
    [Serializable]
    public class UpgradeMap
    {
        [ModelProperty("Options"), SerializeField]
        private List<UpgradeOptions> _upgradeOptions = new List<UpgradeOptions>();
        public IEnumerable<UpgradeOptions> Options => _upgradeOptions;

        public UpgradeOptions GetUpgradeOptions(Tier tier) => _upgradeOptions.FirstOrDefault(x => x.Tier.Equals(tier));

        public IEnumerable<Tier> GetNext(Tier tier) {
            var options = GetUpgradeOptions(tier);
            if (options == null)
            {
                return Array.Empty<Tier>();
            }
            else
            {
                return options.NextTiers;
            }
        }

        public void AddTierToUpgradeOptions(Tier tier, Tier toAdd)
        {
            var options = GetUpgradeOptions(tier);
            if (options == null)
            {
                options = new UpgradeOptions(tier);
                _upgradeOptions.Add(options);
            }
            options.AddTier(toAdd);
        }

        public void RemoveTierFromUpgradeOptions(Tier tier, Tier toRemove) => GetUpgradeOptions(tier).RemoveTier(toRemove);
    }
}