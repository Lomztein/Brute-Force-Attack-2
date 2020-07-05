using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.World.Tiles.Rendering
{
    public class TileMeshUVProvider : MonoBehaviour, ITileMeshUVProvider
    {
        public int Textures;
        public float Margin = 0.001f;

        public Vector2[] GetUVs(int bitmask)
        {
            float texLength = GetTextureUVLength();
            float x = texLength * bitmask;

            if (bitmask > Textures)
            {
                x = 0f;
            }

            //Debug.Log(x + " + " + texLength + " = " + x + texLength);

            return new Vector2[4]
                {
                    new Vector2(x + Margin, 0f), // 0, 0
                    new Vector2(x + texLength - Margin, 0f), // 1, 0
                    new Vector2(x + Margin, 1f), // 0, 1
                    new Vector2(x + texLength - Margin, 1f) // 1, 1
                };
        }

        private float GetTextureUVLength() => 1f / Textures;
    }
}
