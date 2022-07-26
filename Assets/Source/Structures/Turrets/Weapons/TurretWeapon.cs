using Lomztein.BFA2.Serialization;

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