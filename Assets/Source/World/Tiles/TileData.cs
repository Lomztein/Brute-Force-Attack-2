using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
