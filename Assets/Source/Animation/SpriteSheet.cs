using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2
{
    [System.Serializable]
    public class SpriteSheet
    {
        [ModelAssetReference]
        public Texture2D SpriteTexture;
        [ModelProperty]
        public int SpritePPU = 16;
        [ModelProperty]
        public int SpriteWidth = 16;
        [ModelProperty]
        public int SpriteHeight = 16;
        [ModelProperty]
        public Vector2 SpritePivot = new Vector2(0.5f, 0.5f);

        private Sprite[,] _spriteCache;

        public int HorizontalSprites => SpriteTexture.width / SpriteWidth;
        public int VerticalSprites => SpriteTexture.height / SpriteHeight;

        public Sprite this[int x, int y = 0]  => GetSprite(x, y);
        public Sprite this[Vector2Int index]  => GetSprite(index);

        private bool CacheSprites ()
        {
            if (_spriteCache == null)
            {
                _spriteCache = new Sprite[HorizontalSprites, VerticalSprites];
                for (int x = 0; x < HorizontalSprites; x++)
                {
                    for (int y = 0; y < VerticalSprites; y++)
                    {
                        _spriteCache[x, y] = Sprite.Create(SpriteTexture, 
                            new Rect(x * SpriteWidth, y * SpriteHeight, SpriteWidth, SpriteHeight), SpritePivot, SpritePPU);
                    }
                }
                return true;
            }
            return false;
        }

        public Sprite GetSprite (int x, int y = 0)
        {
            CacheSprites();
            return _spriteCache[x, y];
        }

        public IEnumerable<Sprite> GetXRange(int fromX, int toX, int y = 0)
        {
            for (int x = fromX; x < toX; x++)
            {
                yield return GetSprite(x, y);
            }
        }

        public IEnumerable<Sprite> GetSprites(IEnumerable<Vector2Int> indecies)
        {
            foreach (var index in indecies)
            {
                yield return GetSprite(index);
            }
        }

        public Sprite GetSprite(Vector2Int index)
        {
            CacheSprites();
            return _spriteCache[index.x, index.y];
        }
    }
}
