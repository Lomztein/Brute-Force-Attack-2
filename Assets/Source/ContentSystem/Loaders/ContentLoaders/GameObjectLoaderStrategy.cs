using Lomztein.BFA2.ContentSystem.Assemblers;
using Lomztein.BFA2.Serialization;
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
    public class GameObjectLoaderStrategy : IContentLoaderStrategy
    {
        public bool CanLoad(Type type) => type == typeof(GameObject);

        public object Load(string path, Type type)
        {
            var json = JObject.Parse(File.ReadAllText(path));
            var rootModel = ObjectPipeline.DeserializeObject(json);
            GameObjectAssembler assembler = new GameObjectAssembler();
            return assembler.Assemble(rootModel);
        }
    }
}
