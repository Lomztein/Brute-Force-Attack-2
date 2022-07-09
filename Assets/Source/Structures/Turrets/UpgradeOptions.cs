using System;
using System.Collections.Generic;
using System.Linq;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets
{
    [Serializable]
    public class UpgradeOptions : IAssemblable
    {
        public Tier Tier;
        [SerializeField] private List<Tier> _nextTiers = new List<Tier>();
        public IEnumerable<Tier> NextTiers => _nextTiers;

        public UpgradeOptions(Tier tier, params Tier[] next)
        {
            Tier = tier;
            _nextTiers.AddRange(next);
        }

        public UpgradeOptions() { }

        public void AddTier(Tier tier) => _nextTiers.Add(tier);

        public void RemoveTier(Tier tier) => _nextTiers.Remove(tier);

        public ValueModel Disassemble(DisassemblyContext context)
        {
            return new ObjectModel(
                new ObjectField("Tier", new PrimitiveModel(Tier.ToString())),
                new ObjectField("NextTiers", new ArrayModel(_nextTiers.Select(x => new PrimitiveModel(x.ToString()))))
                );
        }

        public void Assemble(ValueModel source, AssemblyContext context)
        {
            ObjectModel model = source as ObjectModel;
            Tier = Tier.Parse(model.GetValue<string>("Tier"));
            _nextTiers = model.GetArray("NextTiers").Select(x => Tier.Parse((x as PrimitiveModel).ToObject<string>())).ToList();
        }
    }
}