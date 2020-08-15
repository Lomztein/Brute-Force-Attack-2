using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Rangers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures
{
    public class SlowfieldGenerator : Structure, IRanger, IModdable
    {
        [ModelProperty]
        public float BaseRange;
        private IStatReference _range;
        [ModelProperty]
        public float SlowFactor;
        [ModelProperty]
        public LayerMask Mask;

        public IStatContainer Stats = new StatContainer();
        public IEventContainer Events = new EventContainer();

        public IModContainer Mods { get; private set; }

        public float GetRange() => _range.GetValue();

        private void Awake()
        {
            Mods = new ModContainer(Stats, Events);
            _range = Stats.AddStat("Range", "Range", "The range of which this slowfield generator generates slowfields.");
            Stats.Init(new StatBaseValues() { BaseValues = new [] { new StatBaseValues.IdentifierValuePair { Identifier = "Range", Value = BaseRange } } });
        }

        public bool IsCompatableWith(IMod mod)
        {
            return mod.ContainsRequiredAttributes(new[] { ModdableAttribute.Ranged });
        }

        private void FixedUpdate()
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, GetRange());
            foreach (Collider2D col in cols)
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.Speed = enemy.MaxSpeed * SlowFactor;
                }
            }
        }
    }
}
