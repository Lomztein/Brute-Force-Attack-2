using Lomztein.BFA2.Enemies;
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
    public class SlowfieldGenerator : Structure, IRanger
    {
        [ModelProperty]
        public float Range;
        [ModelProperty]
        public float SlowFactor;
        [ModelProperty]
        public LayerMask Mask;

        public float GetRange() => Range;

        private void FixedUpdate()
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, Range);
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
