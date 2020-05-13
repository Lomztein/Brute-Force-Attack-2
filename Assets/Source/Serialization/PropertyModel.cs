using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization
{
    public class PropertyModel : IPropertyModel
    {
        public PropertyModel () { }

        public PropertyModel (string name, JToken value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public JToken Value { get; private set; }

        public void Deserialize(JToken data)
        {
            Name = data["Name"].ToString();
            Value = data["Value"];
        }

        public JToken Serialize()
        {
            return new JObject()
            {
                { "Name", new JValue (Name) },
                { "Value",  Value is ISerializable val ? JToken.Parse (val.Serialize().ToString ()) : JToken.FromObject(Value) }
            };
        }
    }
}
