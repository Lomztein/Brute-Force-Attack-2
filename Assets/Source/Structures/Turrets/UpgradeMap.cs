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

        public UpgradeOptions GetUpgradeOptions(Tier tier) => _upgradeOptions.FirstOrDefault(x => x.Tier.Equals(tier));

        public IEnumerable<Tier> GetNext(Tier tier) => GetUpgradeOptions(tier)?.NextTiers ?? Array.Empty<Tier>();

        public void AddTierToUpgradeOptions(Tier tier, Tier toAdd) => GetUpgradeOptions(tier).AddTier(toAdd);

        public void RemoveTierFromUpgradeOptions(Tier tier, Tier toRemove) => GetUpgradeOptions(tier).AddTier(toRemove);
    }
}