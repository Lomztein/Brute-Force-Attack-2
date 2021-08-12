using System;
using System.Collections.Generic;
using Lomztein.BFA2.Serialization;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets
{
    [Serializable]
    public class UpgradeOptions
    {
        public string name => Tier.ToString();

        [ModelProperty]
        public Tier Tier;
        [ModelProperty("Next"), SerializeField]
        private List<Tier> _nextTiers;
        public IEnumerable<Tier> NextTiers => _nextTiers;

        public UpgradeOptions(Tier tier, params Tier[] next)
        {
            Tier = tier;
            _nextTiers = new List<Tier>();
            _nextTiers.AddRange(next);
        }

        public UpgradeOptions() { }

        public void AddTier(Tier tier) => _nextTiers.Add(tier);

        public void RemoveTier(Tier tier) => _nextTiers.Remove(tier);
    }
}