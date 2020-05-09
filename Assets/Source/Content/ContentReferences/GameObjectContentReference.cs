using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.DataStruct;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Content.ContentReferences
{
    [Serializable]
    public class GameObjectContentReference : ISerializable
    {
        public string Path;

        public void Deserialize(IDataStruct data)
        {
            Path = data.ToObject<string>();
        }

        public IDataStruct Serialize()
        {
            return new JsonDataStruct (new JValue(Path));
        }

        public GameObject Get()
            => Content.Get(Path, typeof(GameObject)) as GameObject;
    }
}
