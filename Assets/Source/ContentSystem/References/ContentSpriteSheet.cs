using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.References
{
    [Serializable]
    public class ContentSpriteSheet
    {
        [ModelProperty]
        public ContentSpriteReference[] Sprites = new ContentSpriteReference[] { new ContentSpriteReference() };

        public Sprite GetSprite(int index) => Sprites[index].Get();

        public Sprite GetSprite(float normalized) => Sprites[Mathf.Clamp (Mathf.RoundToInt(normalized * Sprites.Length), 0, Sprites.Length - 1)].Get();
    }
}
