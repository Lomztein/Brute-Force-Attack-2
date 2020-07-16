using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles
{
    public class TileData : ISerializable
    {
        public int Width;
        public int Height;

        public TileTypeReference[,] Tiles;

        public TileData (int width, int height)
        {
            Width = width;
            Height = height;
            ResetTiles(TileType.Empty);
        }

        public TileTypeReference GetTile(int x, int y) => Tiles[x, y];

        public void SetTile(int x, int y, TileType type) => Tiles[x, y] = new TileTypeReference(type.Name);

        public TileTypeReference[,] GetTiles (Vector2Int from, Vector2Int to)
        {
            (from, to) = NormalizeRect(from, to);

            from = ClampVector(from, Vector2Int.zero, new Vector2Int(Width - 1, Height - 1));
            to = ClampVector(to, Vector2Int.zero, new Vector2Int(Width - 1, Height - 1));

            TileTypeReference[,] tiles = new TileTypeReference[to.x - from.x + 1, to.y - from.y + 1];
            int xDelta = to.x - from.x;
            int yDelta = to.y - from.y;

            for (int y = 0; y <= yDelta; y++)
            {
                for (int x = 0; x <= xDelta; x++)
                {
                    int xx = x + from.x;
                    int yy = y + from.y;

                    tiles[x, y] = GetTile(xx, yy);
                }
            }

            return tiles;
        }

        public void SetTiles (Vector2Int from, Vector2Int to, TileType type)
        {
            (from, to) = NormalizeRect(from, to);

            from = ClampVector(from, Vector2Int.zero, new Vector2Int(Width - 1, Height - 1));
            to = ClampVector(to, Vector2Int.zero, new Vector2Int(Width - 1, Height - 1));

            for (int y = from.y; y <= to.y; y++)
            {
                for (int x = from.x; x <= to.x; x++)
                {
                    SetTile(x, y, type);
                }
            }
        }

        public void ReplaceTiles(Vector2Int from, Vector2Int to, TileType toReplace, TileType replacer)
        {
            (from, to) = NormalizeRect(from, to);

            from = ClampVector(from, Vector2Int.zero, new Vector2Int(Width - 1, Height - 1));
            to = ClampVector(to, Vector2Int.zero, new Vector2Int(Width - 1, Height - 1));

            for (int y = from.y; y <= to.y; y++)
            {
                for (int x = from.x; x <= to.x; x++)
                {
                    if (GetTile(x, y).IsType(toReplace))
                    {
                        SetTile(x, y, replacer);
                    }
                }
            }
        }

        private (Vector2Int from, Vector2Int to) NormalizeRect (Vector2Int from, Vector2Int to)
        {
            Vector2Int f = new Vector2Int(
                Mathf.Min(from.x, to.x),
                Mathf.Min(from.y, to.y)
                );

            Vector2Int t = new Vector2Int(
                Mathf.Max(from.x, to.x),
                Mathf.Max(from.y, to.y)
                );

            return (f, t);
        }

        private Vector2Int ClampVector(Vector2Int vec, Vector2Int min, Vector2Int max)
        {
            return new Vector2Int(
                Mathf.Clamp(vec.x, min.x, max.x),
                Mathf.Clamp(vec.y, min.y, max.y)
                );
        }

        private void ResetTiles (TileType type)
        {
            Tiles = TwodifyTiles(new TileTypeReference[Width * Height].Select(x => new TileTypeReference(type.Name)).ToArray());
        }

        private TileTypeReference[] EnflattenTiles(TileTypeReference[,] walls)
        {
            TileTypeReference[] flattened = new TileTypeReference[Width * Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    flattened[MapUtils.CoordsToIndex(x, y, Width)] = walls[x, y];
                }
            }
            return flattened;
        }

        private TileTypeReference[,] TwodifyTiles(TileTypeReference[] walls)
        {
            TileTypeReference[,] twodi = new TileTypeReference[Width, Height];
            for (int i = 0; i < walls.Length; i++)
            {
                (int x, int y) = MapUtils.IndexToCoords(i, Width);
                twodi[x, y] = walls[i];
            }
            return twodi;
        }

        public JToken Serialize()
        {
            return new JArray(EnflattenTiles(Tiles).Select(x => x.Serialize()).ToArray());
        }

        public void Deserialize(JToken source)
        {
            Tiles = TwodifyTiles((source as JArray).Select(x => DeserializeTileTypeReference(x)).ToArray());
        }

        private TileTypeReference DeserializeTileTypeReference (JToken token)
        {
            TileTypeReference reference = new TileTypeReference();
            reference.Deserialize(token);
            return reference;
        }
    }
}
