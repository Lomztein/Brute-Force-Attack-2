using Lomztein.BFA2.Weaponary.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary
{
    public class KillInfo
    {
        public HitInfo HitInfo { get; private set; }
        public DamageInfo DamageInfo { get; private set; }

        public bool FromDirectHit => HitInfo != null;

        public KillInfo (HitInfo hitInfo, DamageInfo damageInfo)
        {
            HitInfo = hitInfo;
            DamageInfo = damageInfo;
        }
    }
}
