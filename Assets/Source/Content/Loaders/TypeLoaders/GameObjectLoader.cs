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

namespace Lomztein.BFA2.Content.Loaders.TypeLoaders
{
    public class GameObjectLoader : IRawContentTypeLoader
    {
        public Type ContentType => typeof(GameObject);

        public object Load(string path)
        {
            GameObjectModel model = new GameObjectModel();
            JToken data = DataSerialization.FromFile(path);
            model.Deserialize(data);

            GameObjectAssembler assembler = new GameObjectAssembler();
            return assembler.Assemble(model);
        }
    }
}