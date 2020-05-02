using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders.TypeLoaders
{
    public class GameObjectModelRawContentLoader : IRawContentTypeLoader
    {
        public Type ContentType => typeof(IGameObjectModel);

        public object Load(string path)
        {
            throw new NotImplementedException();
        }
    }
}