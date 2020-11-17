using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class Texture2DLoaderStrategy : IContentLoaderStrategy
    {
        public bool CanLoad(Type type) => type == typeof(Texture2D);

        public object Load(string path, Type type)
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(File.ReadAllBytes(path));
            return texture;
        }
    }
}