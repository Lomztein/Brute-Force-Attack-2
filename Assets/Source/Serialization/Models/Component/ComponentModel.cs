using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.EngineComponentSerializers;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models.Component
{
    public class ComponentModel : IComponentModel
    {
        private static List<Assembly> _typeSourceAssemblies = new List<Assembly>();

        public Type Type { get; private set; }
        private List<IPropertyModel> _properties = new List<IPropertyModel>();

        public ComponentModel() { }

        public ComponentModel(Type type, params IPropertyModel[] properties)
        {
            Type = type;
            _properties = properties.ToList();
        }

        private Type GetType (string typeName)
        {
        Type type = null;

            foreach (Assembly assembly in _typeSourceAssemblies)
            {
                type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            if (type == null)
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in assemblies)
                {
                    type = assembly.GetType(typeName);
                    if (type != null)
                    {
                        _typeSourceAssemblies.Add(assembly);
                        return type;
                    }
                }
            }

            throw new InvalidOperationException("Type '" + typeName + "' not location in any currently loaded assemblies.");
        }

        public void Deserialize(JToken data)
        {
            Type = GetType(data["TypeName"].ToString());
            JToken properties = data["Properties"];

            foreach (JToken property in properties)
            {
                _properties.Add(new PropertyModel (property["Name"].ToString(), property["Value"]));
            }
        }

        public IPropertyModel[] GetProperties() => _properties.ToArray();

        public JToken Serialize()
        {
            return new JObject()
            {
                { "TypeName", new JValue (Type.FullName) },
                { "Properties", new JArray (_properties.Select(x => x.Serialize ()).ToArray ()) }
            };
        }
    }
}
