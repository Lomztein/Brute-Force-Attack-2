using Lomztein.BFA2.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ObjectVisualizers
{
    public class StructureVisualizer : ObjectVisualizerBase<Structure>
    {
        public Transform HighlighterTransform;
        public SpriteRenderer HighlighterSprite;
        public SpriteRenderer DirectionSprite;
        public float MarginFactor = 1.5f;

        public override void Visualize(Structure component)
        {
            Follow(component.transform);
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
            DirectionSprite.color = color;
        }
    }
}
