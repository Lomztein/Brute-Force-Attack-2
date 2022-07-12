using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI
{
    [ExecuteAlways]
    public class LayoutElementMinSizeAsPreferred : MonoBehaviour, ILayoutElement
    {
        public Component LayoutElement;
        private ILayoutElement InternalElement => LayoutElement as ILayoutElement;

        private void Awake()
        {
            if (LayoutElement == null)
                LayoutElement = GetComponent<ILayoutElement>() as Component;
        }

        public float minWidth => InternalElement.preferredWidth;

        public float preferredWidth => InternalElement.preferredWidth;

        public float flexibleWidth => InternalElement.flexibleWidth;

        public float minHeight => InternalElement.preferredHeight;

        public float preferredHeight => InternalElement.preferredHeight;

        public float flexibleHeight => InternalElement.flexibleHeight;

        public int layoutPriority => InternalElement.layoutPriority;

        public void CalculateLayoutInputHorizontal()
        {
            InternalElement.CalculateLayoutInputHorizontal();
        }

        public void CalculateLayoutInputVertical()
        {
            InternalElement.CalculateLayoutInputVertical();
        }
    }
}
