using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.Components
{
    public class ContentSpriteRenderer : MonoBehaviour
    {
        [ModelProperty]
        public ContentSprite Reference;

        private void Awake()
        {
            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = Reference.Get();
            Destroy(this);
        }
    }
}