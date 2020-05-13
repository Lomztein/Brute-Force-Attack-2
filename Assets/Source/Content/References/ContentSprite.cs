using Lomztein.BFA2.Serialization.EngineObjectSerializers;
using Newtonsoft.Json;
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
        public Vector2 Pivot = new Vector2(0.5f, 0.5f);
        public float PixelsPerUnit = 32;

        public void Deserialize(JToken data)
        {
            RectSerializer rSerializer = new RectSerializer();
            Vector2Serializer vSerializer = new Vector2Serializer();

            Path = data["Path"].ToObject<string>();
            Rect = (Rect)rSerializer.Deserialize (data["Rect"]);
            Pivot = (Vector2)vSerializer.Deserialize(data["Pivot"]);
            PixelsPerUnit = data["PixelsPerUnit"].ToObject<int>();
        }

        public JToken Serialize()
        {
            RectSerializer rSerializer = new RectSerializer();
            Vector2Serializer vSerializer = new Vector2Serializer();

            return new JObject()
            {
                {"Path", Path },
                {"Rect", rSerializer.Serialize(Rect) },
                {"Pivot", vSerializer.Serialize(Pivot) },
                {"PixelsPerUnit", PixelsPerUnit },
            };
        }

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
