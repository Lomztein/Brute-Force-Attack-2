﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.EngineComponentSerializers;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization
{
    public class ComponentModel : IComponentModel
    {
        private static Assembly[] _typeSourceAssemblies = new Assembly[]
        {
            typeof (ComponentModel).Assembly,
            typeof (GameObject).Assembly,
            typeof (Collider2D).Assembly,
        };

        public Type Type { get; private set; }
        private List<IPropertyModel> _properties = new List<IPropertyModel>();

        public ComponentModel() { }

        public ComponentModel(Type type, params IPropertyModel[] properties)
        {
            Type = type;
            _properties = properties.ToList();
        }


        public void Deserialize(JToken data)
        {
            Type = null;
            foreach (Assembly assembly in _typeSourceAssemblies)
            {
                if (Type == null)
                {
                    Type = assembly.GetType(data["TypeName"].ToString());
                }
                else
                {
                    break;
                }
            }

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
