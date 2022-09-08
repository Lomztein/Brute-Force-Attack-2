using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.IO;
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

        public object Load(string path, Type type, IEnumerable<string> patches)
        {
            var token = DataSerialization.FromFile(path, patches);
            return ObjectPipeline.BuildObject(token, type);
        }
    }
}
