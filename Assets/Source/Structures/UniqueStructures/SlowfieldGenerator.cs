﻿using Lomztein.BFA2.Enemies;
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
        public StatInfo RangeInfo;
        [ModelProperty]
        public float BaseRange;
        private IStatReference _range;
        [ModelProperty]
        public float SlowFactor;
        [ModelProperty]
        public LayerMask Mask;

        public float GetRange() => _range.GetValue();

        protected override void AwakeInit()
        {
            _range = Stats.AddStat(RangeInfo, BaseRange, this);
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
