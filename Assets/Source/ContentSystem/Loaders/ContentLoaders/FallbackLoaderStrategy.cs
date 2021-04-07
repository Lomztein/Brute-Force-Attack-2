using Lomztein.BFA2.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class FallbackLoaderStrategy : IContentLoaderStrategy
    {
        public bool CanLoad(Type type)
            => true;

        public object Load(string path, Type type)
        {
            string json = File.ReadAllText(path);
            var jtoken = JToken.Parse(json);

            if (typeof(ScriptableObject).IsAssignableFrom(type))
            {
                return ObjectPipeline.BuildScriptableObject(jtoken, type);
            }
            else
            {
                return ObjectPipeline.BuildObject(jtoken, type);
            }
        }
    }
}
