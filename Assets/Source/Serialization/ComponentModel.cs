using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.DataStruct;
using Lomztein.BFA2.Serialization.EngineComponentSerializers;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization
{
    public class ComponentModel : IComponentModel
    {
        private static IEngineComponentSerializer[] _engineComponentSerializers = new IEngineComponentSerializer[]
        {
            new TransformSerializer(),
        };

        public Type Type { get; private set; }
        private List<IPropertyModel> _properties = new List<IPropertyModel>();

        public ComponentModel() { }

        public ComponentModel(Type type, params IPropertyModel[] properties)
        {
            Type = type;
            _properties = properties.ToList();
        }


        public void Deserialize(IDataStruct data)
        {
            Type = Type.GetType (data.GetValue<string>("TypeName"));
            IDataStruct properties = data.Get("Properties");

            foreach (IDataStruct property in properties)
            {
                _properties.Add(new PropertyModel (property.GetValue<string>("Name"), property.GetValue("Value", typeof (object))));
            }
        }

        public IPropertyModel[] GetProperties() => _properties.ToArray();

        public IDataStruct Serialize()
        {
            return new JsonDataStruct(new JObject()
            {
                { "TypeName", new JValue (Type.FullName) },
                { "Properties", new JArray (_properties.Select(x => JObject.Parse (x.Serialize ().ToString ())).ToArray ()) }
            }
            );
        }

        public static IComponentModel Create (Component component)
        {
            var serializer = _engineComponentSerializers.FirstOrDefault(x => x.Type == component.GetType());
            if (serializer != null)
            {
                return serializer.Serialize(component);
            }

            ComponentModel model = new ComponentModel();

            Type componentType = component.GetType();
            model.Type = componentType;

            IEnumerable<FieldInfo> properties = componentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.IsDefined(typeof(ModelPropertyAttribute), true));

            foreach (FieldInfo info in properties)
            {
                model._properties.Add(new PropertyModel(info.Name, info.GetValue(component)));
            }

            return model;
        }
    }
}
