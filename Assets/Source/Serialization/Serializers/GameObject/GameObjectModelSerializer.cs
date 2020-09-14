using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers.GameObject
{
    public class GameObjectModelSerializer : ISerializer<IGameObjectModel>
    {
        public IGameObjectModel Deserialize(JToken value)
        {
            string name = value["Name"].ToString();
            string tag = value["Tag"].ToString();
            int layer = value["Layer"].ToObject<int>();
            bool isStatic = value["Static"].ToObject<bool>();
            List<IGameObjectModel> children = new List<IGameObjectModel>();
            List<IObjectModel> components = new List<IObjectModel>();

            JArray jChildren = value["Children"] as JArray;
            foreach (JToken child in jChildren)
            {
                IGameObjectModel model = Deserialize(child);
                children.Add(model);
            }

            ObjectModelSerializer serializer = new ObjectModelSerializer();
            JArray jComponents = value["Components"] as JArray;
            foreach (JToken component in jComponents)
            {
                IObjectModel model = serializer.Deserialize(component);
                components.Add(model);
            }

            return new GameObjectModel(name, tag, layer, isStatic, children, components);
        }

        public JToken Serialize(IGameObjectModel value)
        {
            ObjectModelSerializer serializer = new ObjectModelSerializer();

            return new JObject()
            {
                { "Name", new JValue(value.Name) },
                { "Tag", new JValue(value.Tag) },
                { "Layer", new JValue(value.Layer) },
                { "Static", new JValue(value.Static) },
                { "Components", new JArray (value.GetComponentModels().Select (x => serializer.Serialize(x)).ToArray ()) },
                { "Children", new JArray (value.GetChildren().Select (x => Serialize(x)).ToArray ()) }
            };
        }
    }
}
