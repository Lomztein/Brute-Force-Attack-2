using Lomztein.BFA2.ContentSystem.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Loaders.ResourceLoaders
{
    public class GameObjectToCachedGameObjectResourceConverter : IResourceTypeConverter
    {
        public Type InputType => typeof(GameObject);
        public Type OutputType => typeof(IContentCachedPrefab);

        public object Convert(object input)
        {
            return new MemoryCachedGameObject(input as GameObject);
        }
    }
}