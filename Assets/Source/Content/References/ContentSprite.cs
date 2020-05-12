using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.DataStruct;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Lomztein.BFA2.Content.References
{
    [Serializable]
    public class ContentSprite : ISerializable
    {
        public string Path;
        private Sprite _cache;

        public Rect Rect;
        public Vector2 Pivot;
        public float PixelsPerUnit;

        public void Deserialize(IDataStruct data)
        {
            Path = data.ToObject<string>();
        }

        public IDataStruct Serialize()
        {
            return new JsonDataStruct(new JValue(Path));
        }

        public Sprite Get()
        {
            if (_cache == null)
            {
                Texture2D texture = (Texture2D)Content.Get(Path, typeof(Texture2D));
                texture.filterMode = FilterMode.Point;

                _cache = Sprite.Create(texture, Rect, Pivot, PixelsPerUnit, 0, SpriteMeshType.FullRect);
            }
            return _cache;
        }
    }
}
