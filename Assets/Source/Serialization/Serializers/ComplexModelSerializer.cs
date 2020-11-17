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
            bool isImplicit = !obj.ContainsKey(PropertyModelSerializer.CS_TYPE_JSON_NAME);

            Type type = isImplicit ? null : ReflectionUtils.GetType(value[PropertyModelSerializer.CS_TYPE_JSON_NAME].ToString());
            List<ObjectField> properties = new List<ObjectField>();

            JToken jProps = isImplicit ? value : (obj.ContainsKey("Properties") ? obj["Properties"] : new JObject());
            foreach (JProperty property in jProps)
            {
                properties.Add(new ObjectField (property.Name, _internalSerializer.Deserialize(property.Value)));
            }

            return new ObjectModel(type, properties.ToArray());
        }

        public JToken Serialize(ObjectModel value)
        {
            IEnumerable<JProperty> properties = value.GetProperties().Select(x => new JProperty(x.Name, _internalSerializer.Serialize(x.Model)));
            bool isImplicit = value.ImplicitType;

            if (isImplicit)
            {
                return new JObject(properties);
            }
            else
            {
                if (properties.Count() > 0)
                {
                    return new JObject()
                    {
                        { PropertyModelSerializer.CS_TYPE_JSON_NAME, new JValue (value.Type.FullName) },
                        { "Properties", new JObject (properties) }
                    };
                }
                else
                {
                    return new JObject()
                    {
                        { PropertyModelSerializer.CS_TYPE_JSON_NAME, new JValue (value.Type.FullName) },
                    };
                }

            }
        }
    }
}