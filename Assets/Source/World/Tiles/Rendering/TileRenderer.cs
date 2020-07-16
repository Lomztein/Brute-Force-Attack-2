using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles.Rendering
{
    public class TileRenderer : MonoBehaviour
    {
        public MeshFilter MeshFilter;
        private ITileMeshGenerator _tileMeshGenerator;

        public void Awake()
        {
            _tileMeshGenerator = GetComponent<ITileMeshGenerator>();
        }

        public void RegenerateMesh (TileData data)
        {
            Mesh mesh = _tileMeshGenerator.GenerateMesh(data);
            MeshFilter.mesh = mesh;
        }
    }
}