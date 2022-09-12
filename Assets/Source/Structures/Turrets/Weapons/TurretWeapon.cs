using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary.Targeting;

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
                TryFire(new TransformTarget(Provider?.GetTarget()), this);
            }
        }
    }
}