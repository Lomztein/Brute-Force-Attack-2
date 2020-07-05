using UnityEngine;

namespace Lomztein.BFA2.World.Tiles.Rendering
{
    public interface ITileMeshGenerator
    {
        Mesh GenerateMesh(TileData data);
    }
}