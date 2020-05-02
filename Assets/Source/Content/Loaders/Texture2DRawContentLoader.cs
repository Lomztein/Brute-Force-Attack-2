using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders
{
    public class Texture2DRawContentLoader : IRawContentLoader
    {
        public Type ContentType => typeof(Texture2D);

        public object Load(string path)
        {
            throw new NotImplementedException();
        }
    }
}