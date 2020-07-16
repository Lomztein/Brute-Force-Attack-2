using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Utilities
{
    public class MapUtils
    {
        public static int CoordsToIndex(int x, int y, int width) => y * width + x;

        public static (int x, int y) IndexToCoords(int index, int width)
        {
            int x = index % width;
            int y = Mathf.FloorToInt((float)index / width);
            return (x, y);
        }

        public static bool IsInsideMap(int x, int y, int width, int height)
        {
            if (x < 0 || x > width - 1)
                return false;
            if (y < 0 || y > height - 1)
                return false;
            return true;
        }

        public static Vector2Int WorldToTileCoords(Vector2 position, int width, int height)
        {
            return new Vector2Int(
                Mathf.RoundToInt(position.x - 0.5f + width / 2f),
                Mathf.RoundToInt(position.y - 0.5f + height / 2f));
        }

        public static Vector3 TileToWorldCoords (Vector2Int position, int width, int height)
        {
            return new Vector3(
                position.x + 0.5f - width / 2f,
                position.y + 0.5f - height / 2f);
        }
    }
}
