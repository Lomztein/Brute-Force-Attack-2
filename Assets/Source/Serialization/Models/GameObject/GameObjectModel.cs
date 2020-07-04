using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Component;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models.GameObject
{
    public class GameObjectModel : IGameObjectModel
    {
        public string Name { get; private set; }
        public string Tag { get; private set; }
        public int Layer { get; private set; }
        public bool Static { get; private set; }

        private List<IGameObjectModel> _children = new List<IGameObjectModel>();
        private List<IComponentModel> _componentModels = new List<IComponentModel>();

        public GameObjectModel() { }

        public GameObjectModel(string name, string tag, int layer, bool @static, IEnumerable<IGameObjectModel> children, IEnumerable<IComponentModel> componentModels)
        {
            Name = name;
            Tag = tag;
            Layer = layer;
            Static = @static;
            _children = children.ToList();
            _componentModels = componentModels.ToList();
        }

        public void Deserialize(JToken data)
        {
            Name = data["Name"].ToString ();
            Tag = data["Tag"].ToString();
            Layer = data["Layer"].ToObject<int>();
            Static = data["Static"].ToObject<bool>();

            JArray children = data["Children"] as JArray;
            foreach (JToken child in children)
            {
                IGameObjectModel model = new GameObjectModel();
                model.Deserialize(child);
                _children.Add(model);
            }

            JArray components = data["Components"] as JArray;
            foreach (JToken component in components)
            {
                ComponentModel model = new ComponentModel();
                model.Deserialize(component);
                _componentModels.Add(model);
            }
        }

        public IComponentModel[] GetComponentModels() => _componentModels.ToArray();
        public IGameObjectModel[] GetChildren() => _children.ToArray();

        // TOOD: It doesn't make sense that serialization is implementation specific, split it into a seperate serializer class.
        public JToken Serialize()
        {
            return new JObject()
            {
                { "Name", new JValue(Name) },
                { "Tag", new JValue(Tag) },
                { "Layer", new JValue(Layer) },
                { "Static", new JValue(Static) },
                { "Components", new JArray (_componentModels.Select (x => x.Serialize()).ToArray ()) },
                { "Children", new JArray (_children.Select (x => x.Serialize()).ToArray ()) }
            };
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
