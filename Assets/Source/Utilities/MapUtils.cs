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
    }
}
