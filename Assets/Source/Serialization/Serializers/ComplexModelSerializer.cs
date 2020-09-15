using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Serializers
{
    public class ComplexModelSerializer : ISerializer<IObjectModel>
    {
        private PropertyModelSerializer _internalSerializer = new PropertyModelSerializer();

        public IObjectModel Deserialize(JToken value)
        {
            JObject obj = value as JObject;
            Type type = obj.ContainsKey("Type") ? ReflectionUtils.GetType(value["Type"].ToString()) : null;
            bool implicitType = type == null; // Shortcuts! :D

            List<ObjectField> properties = new List<ObjectField>();

            JToken jProps = implicitType ? value : value["Properties"];
            foreach (JProperty property in jProps)
            {
                properties.Add(new ObjectField (property.Name, _internalSerializer.Deserialize(property.Value)));
            }

            return new ObjectModel(type, properties.ToArray());
        }

        public JToken Serialize(IObjectModel value)
        {
            bool implicitType = value.Type == null;
            IEnumerable<JProperty> properties = value.GetProperties().Select(x => new JProperty(x.Name, _internalSerializer.Serialize(x.Model)));

            if (implicitType)
            {
                return new JObject(properties);
            }
            else
            {
                return new JObject()
                {
                    { "Type", new JValue (value.Type.FullName) },
                    { "Properties", new JObject (properties) }
                };
            }
        }
    }
}