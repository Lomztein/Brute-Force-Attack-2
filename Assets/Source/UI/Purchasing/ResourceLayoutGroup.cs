using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Purchasing.UI
{
    [ExecuteAlways]
    public class ResourceLayoutGroup : LayoutGroup
    {
        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
            // Yes, this is cobbled together with trail and error.
            // Yes it is garbage.
            // Yes, I do love it.

            RectTransform rect = transform as RectTransform;
            int childCount = transform.childCount;
            float childWidth = rect.rect.width / childCount;
            int index = 0;
            foreach (RectTransform child in rect)
            {
                child.sizeDelta = new Vector2(childWidth, child.sizeDelta.y);
                child.localPosition = new Vector3 (Mathf.Lerp(-rect.rect.width / 2f + m_Padding.right, rect.rect.width / 2f - m_Padding.left, index / (float)(childCount - 1)), 0f);
                index++;
            }
        }

        public override void SetLayoutVertical()
        {
        }
    }
}
