using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Highlighters
{
    public class StructureHighlighter : HighlighterBase<Structure>
    {
        public Transform HighlighterTransform;
        public SpriteRenderer HighlighterSprite;
        public float MarginFactor = 1.5f;

        public override void Highlight(Structure component)
        {
            if (component.transform.parent == null)
            {
                HighlighterTransform.gameObject.SetActive(true);
                float x = World.Grid.SizeOf(component.Width) * MarginFactor;
                float y = World.Grid.SizeOf(component.Height) * MarginFactor;
                HighlighterTransform.localScale = new Vector3(x, y, 1);
            }
            else
            {
                HighlighterTransform.gameObject.SetActive(false);
            }
        }

        public override void Tint(Color color)
        {
            HighlighterSprite.color = color;
        }
    }
}
