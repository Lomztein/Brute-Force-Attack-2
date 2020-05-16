using Lomztein.BFA2.Content.References.GameObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References
{
    [Serializable]
    public class ContentGameObject : IContentGameObject, ISerializable
    {
        public string Path;

        public ContentGameObject () { }

        public ContentGameObject (string path)
        {
            Path = path;
        }

        public GameObject Instantiate()
        {
            GameObject go = UnityEngine.Object.Instantiate(Get());
            go.SetActive(true);
            return go;
        }

        public GameObject Get() => Content.Get(Path, typeof(GameObject)) as GameObject;

        public JToken Serialize()
        {
            return new JValue(Path);
        }

        public void Deserialize(JToken source)
        {
            Path = source.ToString();
        }
    }
}
