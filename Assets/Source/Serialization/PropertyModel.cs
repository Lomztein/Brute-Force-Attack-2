using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.DataStruct;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization
{
    public class PropertyModel : IPropertyModel
    {
        public PropertyModel () { }

        public PropertyModel (string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public object Value { get; private set; }

        public void Deserialize(IDataStruct data)
        {
            Name = data.GetValue<string>("Name");
            Value = data.GetValue<string>("Name");
        }

        public IDataStruct Serialize()
        {
            return new JsonDataStruct(new JObject()
            {
                { "Name", new JValue (Name) },
                { "Value",  JToken.FromObject(Value) }
            });
        }
    }
}
