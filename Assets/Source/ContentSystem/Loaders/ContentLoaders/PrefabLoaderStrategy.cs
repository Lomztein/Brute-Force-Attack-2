using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Serialization.Serializers.GameObject;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class PrefabLoaderStrategy : IContentLoaderStrategy
    {
        public bool CanLoad(Type type)
            => typeof(ContentCachedPrefab).IsAssignableFrom(type);

        public object Load(string path, Type type)
        {
            JToken data = DataSerialization.FromFile(path);
            GameObjectModelSerializer serializer = new GameObjectModelSerializer();
            return new ContentCachedPrefab (serializer.Deserialize(data));
        }
    }
}