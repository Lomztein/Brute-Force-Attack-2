using UnityEngine;

namespace Lomztein.BFA2.World.Tiles.Rendering
{
    public interface ITileMeshUVProvider
    {
        Vector2[] GetUVs(int bitmask);
    }
}