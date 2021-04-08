using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.World
{
    public enum Size
    {
        Small = 1, Medium = 2, Large = 3, Huge = 4
    }

    public static class Grid
    {
        public const float CELL_SIZE = 1f;

        public static bool IsEven(Size size) => (int)size % 2 == 0;

        public static float SizeOf(Size size) => (int)size * CELL_SIZE;

        public static Vector2 SnapToGrid(Vector2 position, Size width, Size height)
        {
            float size = CELL_SIZE;
            bool widthEven = IsEven(width);
            bool heightEven = IsEven(height);

            Vector2 woffset = !widthEven ? new Vector2(size / 2f, size / 2f) : Vector2.zero;
            Vector2 hoffset = !heightEven ? new Vector2(size / 2f, size / 2f) : Vector2.zero;

            float x = Mathf.Round((position.x - woffset.x) / size) * size + woffset.x;
            float y = Mathf.Round((position.y - hoffset.y) / size) * size + hoffset.x;

            return new Vector2(x, y);
        }

        public static Vector2 SnapToGrid(Vector2 position, Size size) => SnapToGrid(position, size, size);
    }
}
