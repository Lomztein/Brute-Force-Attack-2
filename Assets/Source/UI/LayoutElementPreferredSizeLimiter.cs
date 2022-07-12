using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2
{
    public class LayoutElementPreferredSizeLimiter : MonoBehaviour, ILayoutElement
    {
        public Component LayoutElement;

        public float WidthLimit;
        public float HeightLimit;

        public Component WidthSourceElement;
        private ILayoutElement InternalWidthSourceElement => WidthSourceElement as ILayoutElement;

        public Component HeightSourceElement;
        private ILayoutElement InternalHeightSourceElement => HeightSourceElement as ILayoutElement;

        public bool MaxWidth;
        public bool MaxHeight;

        public float minWidth => InternalElement.minWidth;

        public float preferredWidth => Mathf.Min (InternalElement.preferredWidth, GetWidthLimit());

        public float flexibleWidth => InternalElement.flexibleWidth;

        public float minHeight => InternalElement.minHeight;

        public float preferredHeight => Mathf.Min (InternalElement.preferredHeight, GetHeightLimit());

        public float flexibleHeight => InternalElement.flexibleHeight;

        public int layoutPriority => InternalElement.layoutPriority;

        private ILayoutElement InternalElement => LayoutElement as ILayoutElement;

        private float GetWidthLimit ()
        {
            if (WidthSourceElement == null) return WidthLimit;
            if (MaxWidth)
                return Mathf.Max(WidthLimit, InternalWidthSourceElement.preferredWidth);
            else
                return Mathf.Min(WidthLimit, InternalWidthSourceElement.preferredWidth);
        }

        private float GetHeightLimit ()
        {
            if (HeightSourceElement == null) return WidthLimit;
            if (MaxHeight)
                return Mathf.Max(HeightLimit, InternalHeightSourceElement.preferredWidth);
            else
                return Mathf.Min(HeightLimit, InternalHeightSourceElement.preferredWidth);
        }

        public void CalculateLayoutInputHorizontal()
        {
            InternalElement.CalculateLayoutInputHorizontal();
        }

        public void CalculateLayoutInputVertical()
        {
            InternalElement.CalculateLayoutInputVertical();
        }

        private void Awake()
        {
            if (LayoutElement == null)
                LayoutElement = GetComponent<ILayoutElement>() as Component;

            if (WidthSourceElement == null)
                WidthSourceElement = GetComponent<ILayoutElement>() as Component;

            if (HeightSourceElement == null)
                HeightSourceElement = GetComponent<ILayoutElement>() as Component;
        }
    }
}
