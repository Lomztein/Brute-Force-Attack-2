using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders
{
    public class GameObjectModelRawContentLoader : IRawContentLoader
    {
        public Type ContentType => typeof(IGameObjectModel);

        public object Load(string path)
        {
            throw new NotImplementedException();
        }
    }
}