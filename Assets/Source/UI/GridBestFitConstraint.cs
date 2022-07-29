using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI
{
    [ExecuteAlways]
    public class GridBestFitConstraint : MonoBehaviour
    {
        public enum SizePriority { Min, Max }

        public GridLayoutGroup Layout;
        public int MinSize;
        public int MaxSize;
        public SizePriority Priority;

        private void Update()
        {
            int childCount = transform.childCount;
            if (childCount != 0)
            {
                Layout.constraintCount = GetBestFit(childCount);
            }
        }

        private int GetBestFit (int childCount)
        {
            var options = Enumerable.Range(MinSize, MaxSize - MinSize);
            if (Priority == SizePriority.Max)
            {
                options = options.Reverse();
            }

            int bestFit = int.MaxValue;
            int fitSize = -1;
            foreach (var option in options)
            {
                int match = Match(childCount, option);
                if (match < bestFit)
                {
                    bestFit = match;
                    fitSize = option;
                }
            }

            return fitSize;
        }

        private static int Match (int childCount, int constraintSize)
            => childCount % constraintSize;
    }
}
