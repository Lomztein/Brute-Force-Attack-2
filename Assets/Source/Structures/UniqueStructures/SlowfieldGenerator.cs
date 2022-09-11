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
        [ModelAssetReference]
        public StatInfo RangeInfo;
        [ModelProperty]
        public float BaseRange;
        private IStatReference _range;

        [ModelAssetReference]
        public StatInfo SlowFactorInfo;
        [ModelProperty]
        public float BaseSlowFactor;
        private IStatReference _slowFactor;

        [ModelProperty]
        public LayerMask Mask;

        public float GetRange() => _range.GetValue();

        protected override void AwakeInit()
        {
            _range = Stats.AddStat(RangeInfo, BaseRange, this);
            _slowFactor = Stats.AddStat(SlowFactorInfo, BaseSlowFactor, this);
        }

        private void FixedUpdate()
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, GetRange(), Mask);
            foreach (Collider2D col in cols)
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.Speed = enemy.MaxSpeed * (1f - _slowFactor.GetValue());
                }
            }
        }
    }
}
