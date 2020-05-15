using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models.Turret
{
    public interface ITurretComponentModel : ISerializable
    {
        string ComponentIdentifier { get; }
        Vector3 RelativePosition { get; }

        ITurretComponentModel[] GetChildren();
    }
}