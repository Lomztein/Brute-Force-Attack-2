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

            float woffset = !widthEven ? size / 2f : 0f;
            float hoffset = !heightEven ? size / 2f : 0f;

            float x = Mathf.Round((position.x - woffset) / size) * size + woffset;
            float y = Mathf.Round((position.y - hoffset) / size) * size + hoffset;

            return new Vector2(x, y);
        }

        public static Vector2 SnapToGrid(Vector2 position, Size size) => SnapToGrid(position, size, size);

        public static IEnumerable<Vector2> GenerateGridPoints (Vector2 position, Size width, Size height)
        {
            int w = (int)width;
            int h = (int)height;
            Vector3 offset = new Vector3(w - 1, h - 1) / 2f;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    yield return position + new Vector2(x - offset.x, y - offset.y) * CELL_SIZE;
                }
            }
        }
    }
}
