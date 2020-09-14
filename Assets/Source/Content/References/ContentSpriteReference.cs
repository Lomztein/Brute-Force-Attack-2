using Lomztein.BFA2.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Lomztein.BFA2.Content.References
{
    [Serializable]
    public class ContentSpriteReference
    {
        [ModelProperty]
        public string Path;
        private Sprite _cache;

        [ModelProperty]
        public Rect Rect;
        [ModelProperty]
        public Vector2 Pivot = new Vector2(0.5f, 0.5f);
        [ModelProperty]
        public float PixelsPerUnit = 32;

        public Sprite Get()
        {
            if (_cache == null)
            {
                Texture2D texture = (Texture2D)Content.Get(Path, typeof(Texture2D));
                texture.filterMode = FilterMode.Point;
                Rect rect = Rect.size.magnitude > 0.1f ? Rect : new Rect(0f, 0f, texture.width, texture.height);

                _cache = Sprite.Create(texture, rect, Pivot, PixelsPerUnit, 0, SpriteMeshType.FullRect);
            }
            return _cache;
        }
    }
}
