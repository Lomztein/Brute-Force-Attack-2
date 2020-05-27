using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders.ResourceLoaders
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