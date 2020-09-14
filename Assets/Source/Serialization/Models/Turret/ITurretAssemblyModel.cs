namespace Lomztein.BFA2.Serialization.Models.Turret
{
    public interface ITurretAssemblyModel
    {
        string Description { get; }
        string Name { get; }
        ITurretComponentModel RootComponent { get; }
    }
}