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

        private void Start()
        {
            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            Debug.Log(Reference);
            renderer.sprite = Reference.Get();
            Destroy(this);
        }
    }
}