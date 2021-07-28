using Lomztein.BFA2.Visuals.FireAnimations;
using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Pooling;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Attachment;
using Lomztein.BFA2.Structures.Turrets.Rangers;
using Lomztein.BFA2.Structures.Turrets.Targeters;
using Lomztein.BFA2.Structures.Turrets.TargetProviders;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.Weaponary;
using Lomztein.BFA2.Weaponary.FireControl;
using Lomztein.BFA2.Weaponary.FireSynchronization;
using Lomztein.BFA2.Weaponary.Projectiles;
using Lomztein.BFA2.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = Lomztein.BFA2.Colorization.Color;

namespace Lomztein.BFA2.Structures.Turrets.Weapons
{
    public class TurretWeapon : TurretWeaponBase
    {
        [ModelProperty]
        public float FireTreshold;

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            if (Targeter != null && Targeter.GetDistance() < FireTreshold)
            {
                Weapon.Target = Provider?.GetTarget();
                TryFire();
            }
        }
    }
}