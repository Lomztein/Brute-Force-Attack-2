using Lomztein.BFA2.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles.Rendering
{
    public class TileMeshGenerator : MonoBehaviour, ITileMeshGenerator
    {
        public TileTypeReference GenerateType;
        private ITileMeshUVProvider _uvProvider;
        private Vector2Int[] _nearby = new Vector2Int[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1),
        };

        private ITileMeshUVProvider GetUVProvider ()
        {
            if (_uvProvider == null)
            {
                _uvProvider = GetComponent<ITileMeshUVProvider>();
            }
            return _uvProvider;
        }

        public Mesh GenerateMesh(TileData data)
        {
            int quads = data.Width * data.Height;

            Vector3[] verts = new Vector3[quads * 4];
            Vector2[] uvs = new Vector2[verts.Length];
            int[] tris = new int[quads * 2 * 3];

            for (int y = 0; y < data.Height; y++)
            {
                for (int x = 0; x < data.Width; x++)
                {
                    if (data.Tiles[x,y].WallType == GenerateType.WallType)
                    {
                        GenerateQuad(x, y, verts, uvs, tris, data.Width, CalculateBitmask(data, x, y));
                    }
                }
            }

            Vector3[] norms = verts.Select(x => Vector3.back).ToArray();

            Mesh mesh = new Mesh();
            mesh.vertices = verts;
            mesh.normals = norms;
            mesh.uv = uvs;
            mesh.triangles = tris;

            mesh.RecalculateBounds();

            return mesh;
        }

        private void GenerateQuad(int x, int y, Vector3[] verts, Vector2[] uvs, int[] tris, int width, int bitmask)
        {
            int quadIndex = MapUtils.CoordsToIndex(x, y, width);
            int vertIndex = quadIndex * 4;
            int triIndex = quadIndex * 6;

            int vertWidth = width * 2;

            // Generate verts
            verts[vertIndex] = new Vector3(x, y);
            verts[vertIndex + 1] = new Vector3(x + 1, y);
            verts[vertIndex + 2] = new Vector3(x, y + 1);
            verts[vertIndex + 3] = new Vector3(x + 1, y + 1);

            // Generate UVs
            Vector2[] uv = GetUVProvider().GetUVs(bitmask);
            uvs[vertIndex] = uv[0];
            uvs[vertIndex + 1] = uv[1];
            uvs[vertIndex + 2] = uv[2];
            uvs[vertIndex + 3] = uv[3];

            // Generate tris
            tris[triIndex + 2] = vertIndex;
            tris[triIndex + 1] = vertIndex + 1;
            tris[triIndex + 0] = vertIndex + 2;

            tris[triIndex + 5] = vertIndex + 1;
            tris[triIndex + 4] = vertIndex + 3;
            tris[triIndex + 3] = vertIndex + 2;
        }

        private class Quad
        {
            public Vector3[] Verts;
            public Vector2[] UVs;
            public int[] Tris;

            public Quad (Vector3[] verts, Vector2[] uvs, int[] tris)
            {
                Verts = verts;
                UVs = uvs;
                Tris = tris;
            }
        }

        private int CalculateBitmask(TileData data, int x, int y)
        {
            int bitmask = 0;

            for (int i = 0; i < _nearby.Length; i++)
            {
                int xx = x + _nearby[i].x;
                int yy = y + _nearby[i].y;

                if (MapUtils.IsInsideMap(xx, yy, data.Width, data.Height))
                {
                    if (data.Tiles[xx, yy].WallType == GenerateType.WallType)
                    {
                        bitmask += Mathf.RoundToInt(Mathf.Pow(2, i));
                    }
                }
            }

            return bitmask;
        }
    }
}