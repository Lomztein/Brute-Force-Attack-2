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
    public class ComplexModelSerializer
    {
        private PropertyModelSerializer _internalSerializer = new PropertyModelSerializer();

        public ObjectModel Deserialize(JToken value)
        {
            JObject obj = value as JObject;
            List<ObjectField> properties = new List<ObjectField>();

            JToken jProps = value;
            foreach (JProperty property in jProps)
            {
                properties.Add(new ObjectField (property.Name, _internalSerializer.Deserialize(property.Value)));
            }

            return new ObjectModel(type, properties.ToArray());
        }

        public JToken Serialize(ObjectModel value)
        {
            IEnumerable<JProperty> properties = value.GetProperties().Select(x => new JProperty(x.Name, _internalSerializer.Serialize(x.Model)));
            return new JObject(properties);
        }
    }
}