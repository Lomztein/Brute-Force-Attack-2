using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles.Rendering
{
    public class TileRenderer : MonoBehaviour
    {
        public MeshFilter MeshFilter;
        private ITileMeshGenerator _tileMeshGenerator;

        public ITileMeshGenerator GetGenerator()
        {
            if (_tileMeshGenerator == null)
            {
                _tileMeshGenerator = GetComponent<ITileMeshGenerator>();
            }
            return _tileMeshGenerator;
        }

        public void RegenerateMesh (TileData data)
        {
            Mesh mesh = GetGenerator().GenerateMesh(data);
            MeshFilter.mesh = mesh;
        }
    }
}