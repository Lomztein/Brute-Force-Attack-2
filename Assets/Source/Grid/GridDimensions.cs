using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Grid
{
    public enum Size
    {
        Small = 1, Medium = 2, Large = 3, Huge = 4
    }

    public static class GridDimensions
    {
        public const float CELL_SIZE = 1f;

        public static bool IsEven(Size size) => (int)size % 2 == 0;

        public static float SizeOf(Size size) => (int)size * CELL_SIZE;
    }
}
