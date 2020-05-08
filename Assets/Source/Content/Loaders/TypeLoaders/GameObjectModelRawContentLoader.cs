using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.DataStruct;
using Lomztein.BFA2.Serialization.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Content.Loaders.TypeLoaders
{
    public class GameObjectModelRawContentLoader : IRawContentTypeLoader
    {
        public Type ContentType => typeof(IGameObjectModel);

        public object Load(string path)
        {
            GameObjectModel model = new GameObjectModel();
            IDataStruct data = DataParse.FromFile(path);
            model.Deserialize(data);
            return model;
        }
    }
}