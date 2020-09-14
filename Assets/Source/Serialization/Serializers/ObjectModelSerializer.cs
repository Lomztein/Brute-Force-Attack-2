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
    public class ObjectModelSerializer : ISerializer<IObjectModel>
    {
        private PropertyModelSerializer _internalSerializer = new PropertyModelSerializer();

        public IObjectModel Deserialize(JToken value)
        {
            Type type = ReflectionUtils.GetType(value["Type"].ToString());
            List<ObjectField> properties = new List<ObjectField>();

            foreach (JToken property in value["Properties"])
            {
                properties.Add(new ObjectField (property["Name"].ToString(), _internalSerializer.Deserialize(property["Model"])));
            }

            return new ObjectModel(type, properties.ToArray());
        }

        public JToken Serialize(IObjectModel value)
        {
            return new JObject()
            {
                { "Type", new JValue (value.Type.FullName) },
                { "Properties", new JArray (value.GetProperties().Select(x =>
                    new JObject ()
                    {
                        {"Name", x.Name },
                        {"Model", _internalSerializer.Serialize(x.Model) }
                    }).ToArray())
                }
            };
        }
    }
}