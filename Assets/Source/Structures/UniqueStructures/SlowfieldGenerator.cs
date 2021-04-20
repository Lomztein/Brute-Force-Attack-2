using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Rangers;
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
        [ModelReference]
        public SlowfieldGenerator MeMyselfAndI;

        public float GetRange() => _range.GetValue();

        protected override void Awake()
        {
            base.Awake();
            _range = Stats.AddStat("Range", "Range", "The range of which this slowfield generator generates slowfields.", BaseRange);
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
