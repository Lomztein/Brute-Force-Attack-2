using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.ContentSystem.References.ReferenceComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Objects.Assemblers.ComponentConverters
{
    public class SpriteRendererConverter : ComponentConverterBase<SpriteRenderer>
    {
        public override Component ConvertComponent(SpriteRenderer component, GameObject target)
        {
            ContentSpriteRenderer renderer = target.AddComponent<ContentSpriteRenderer>();
            Sprite sprite = component.sprite;

            renderer.Reference = new ContentSpriteReference();
            renderer.Reference.Texture = sprite.texture;
            renderer.Reference.Rect = sprite.rect;
            renderer.Reference.PixelsPerUnit = sprite.pixelsPerUnit;
            renderer.Reference.Pivot = new Vector2(sprite.pivot.x / sprite.rect.width, sprite.pivot.y / sprite.rect.height);

            renderer.Color = component.color;

            UnityEngine.Object.DestroyImmediate(component);
            return renderer;
        }
    }
}
