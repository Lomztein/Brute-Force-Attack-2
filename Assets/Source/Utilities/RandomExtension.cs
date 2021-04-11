using System;

namespace Util
{
    public static class RandomExtension
    {
        public static double Range(this Random random, double min, double max)
        {
            return min + random.NextDouble() * (max - min);
        }
    }
}