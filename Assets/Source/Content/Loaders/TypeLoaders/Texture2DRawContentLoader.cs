using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders.TypeLoaders
{
    public class Texture2DRawContentLoader : IRawContentTypeLoader
    {
        public Type ContentType => typeof(Texture2D);

        public object Load(string path)
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(File.ReadAllBytes(path));
            return texture;
        }
    }
}