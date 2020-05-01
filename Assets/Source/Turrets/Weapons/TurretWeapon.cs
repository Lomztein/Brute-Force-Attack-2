using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Targeters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Weapons
{
    public class TurretWeapon : TurretComponent, IWeapon
    {
        [TurretComponent]
        public ITargeter Targeter;
        [ModelProperty]
        public float FireTreshold;

        public override void End()
        {
        }

        public override void Init()
        {
        }

        public override void Tick(float deltaTime)
        {
            if (Targeter != null && Targeter.GetDistance () < FireTreshold)
            {
            }
        }
    }
}