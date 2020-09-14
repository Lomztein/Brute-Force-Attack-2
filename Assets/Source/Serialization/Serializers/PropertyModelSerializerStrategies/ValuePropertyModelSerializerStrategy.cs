using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Serialization.Serializers.PropertyModelSerializerStrategies.EngineObjectSerializers;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Serializers.PropertyModelSerializerStrategies
{
    public class ValuePropertyModelSerializerStrategy : IPropertyModelSerializerStrategy
    {
        public bool CanSerialize(Type type) => type.IsValueType;

        public IPropertyModel Deserialize(JToken token, Type type)
        {
            return new ValuePropertyModel(
                token["Name"].ToString(),
                token["Value"].ToObject(type)
                );
        }

        public JToken Serialize(IPropertyModel model)
        {
            ValuePropertyModel valueModel = model as ValuePropertyModel;
            return new JObject()
            {
                { "Type", model.Type.FullName },
                { "Name", model.Name },
                { "Value", JToken.FromObject(valueModel.Value) }
            };
        }
    }
}
