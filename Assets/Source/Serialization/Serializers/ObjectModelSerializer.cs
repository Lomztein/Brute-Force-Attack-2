using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Component;
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
    public class ObjectModelSerializer : ISerializer<IObjectModel>
    {
        private PropertyModelSerializer _internalSerializer = new PropertyModelSerializer();

        public IObjectModel Deserialize(JToken value)
        {
            Type type = ReflectionUtils.GetType(value["Type"].ToString());
            List<IPropertyModel> properties = new List<IPropertyModel>();

            foreach (JToken property in value["Properties"])
            {
                properties.Add(_internalSerializer.Deserialize(property));
            }

            return new ObjectModel(type, properties.ToArray());
        }

        public JToken Serialize(IObjectModel value)
        {
            return new JObject()
            {
                { "Type", new JValue (value.Type.FullName) },
                { "Properties", new JArray (value.GetProperties().Select(x => _internalSerializer.Serialize(x).ToArray ())) }
            };
        }
    }
}