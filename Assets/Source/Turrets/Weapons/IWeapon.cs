namespace Lomztein.BFA2.Turrets.Weapons
{
    public interface IWeapon
    {
        float GetDamage();
        float GetFirerate();
        int GetProjectileAmount();
        float GetSpeed();
        float GetSpread();
        float GetRange();
        bool TryFire();
    }
}