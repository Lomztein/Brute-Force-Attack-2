using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class ObjectLoaderStrategy : IContentLoaderStrategy
    {
        public bool CanLoad(Type type)
            => !type.IsValueType;

        public object Load(string path, Type type)
        {
            string json = File.ReadAllText(path);
            return ObjectPipeline.BuildObject(JToken.Parse(json), type);
        }
    }
}
