using Lomztein.BFA2.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.References
{
    [Serializable]
    public class ContentSpriteReference
    {
        [ModelProperty, Obsolete("Use ContentSpriteReference.Texture instead.")]
        public string Path;
        [ModelAssetReference]
        public Texture2D Texture;

        private Sprite _cache;

        [ModelProperty]
        public Rect Rect;
        [ModelProperty]
        public Vector2 Pivot = new Vector2(0.5f, 0.5f);
        [ModelProperty]
        public float PixelsPerUnit = 16;

        public Sprite Get()
        {
            if (_cache == null)
            {
                Texture2D texture;

                if (Texture != null)
                {
                    texture = Texture;
                }
                else if (!string.IsNullOrEmpty(Path))
                {
                    try
                    {
                        texture = (Texture2D)Content.Get(Path, typeof(Texture2D));
                    }
                    catch (FileNotFoundException)
                    {
                        texture = new Texture2D(2, 2);
                    }
                }
                else
                {
                    Debug.LogWarning("Texture missing!");
                    texture = new Texture2D(2, 2);
                }

                texture.filterMode = FilterMode.Point;
                Rect rect = Rect.size.magnitude > 0.1f ? Rect : new Rect(0f, 0f, texture.width, texture.height);

                if (
                    rect.x + rect.width > texture.width ||
                    rect.y + rect.height > texture.height
                    )
                {
                    rect = new Rect(0, 0, texture.width, texture.height);
                }

                _cache = Sprite.Create(texture, rect, Pivot, PixelsPerUnit, 0, SpriteMeshType.FullRect);
            }
            return _cache;
        }
    }
}
