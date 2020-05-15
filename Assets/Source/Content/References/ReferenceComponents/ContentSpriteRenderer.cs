using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.Componentsnny
{
    public class ContentSpriteRenderer : MonoBehaviour
    {
        [ModelProperty]
        public ContentSprite Reference;

        public void OnAssembled()
        {
            Convert();
        }

        private void Start()
        {
            Convert();
        }

        private void Convert ()
        {
            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = Reference.Get();
            DestroyImmediate(this, true);
        }
    }
}