using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.ReferenceComponents
{
    public class ContentSpriteRenderer : ReferenceComponentBase
    {
        [ModelProperty]
        public ContentSpriteReference Reference;
        [ModelProperty]
        public Color Color = Color.white;

        protected override void Apply()
        {
            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = Reference.Get();
            renderer.color = Color;
        }
    }
}