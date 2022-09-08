using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization.IO;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Serializers;
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
            => typeof(IContentCachedPrefab) == type || type == typeof (IContentPrefab);

        public object Load(string path, Type type, IEnumerable<string> patches)
        {
            JToken data = DataSerialization.FromFile(path, patches);
            var serializer = new RootSerializer();
            var model = serializer.Deserialize(data);
            return new ContentCachedPrefab (model);
        }
    }
}