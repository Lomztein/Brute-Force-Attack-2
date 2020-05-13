using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq; 
using UnityEngine;

namespace Lomztein.BFA2.Content.References
{
    [Serializable]
    public class ContentGameObject : ISerializable
    {
        public string Path;

        public void Deserialize(JToken data)
        {
            Path = data.ToObject<string>();
        }

        public JToken Serialize()
        {
            return new JValue(Path);
        }

        public GameObject Get()
            => Content.Get(Path, typeof(GameObject)) as GameObject;

        public GameObjectPrefab GetPrefab ()
            => new GameObjectPrefab(Get());
    }
}
