namespace Lomztein.BFA2.Structures.Turrets.Weapons
{
    public interface IWeapon
    {
        float GetDamage();
        float GetFirerate();
        int GetProjectileAmount();
        int GetMuzzleAmount ();
        float GetSpeed();
        float GetSpread();
        float GetRange();
        bool TryFire();
    }
}