using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Serializers.ModelSerializerStrategies;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers
{
    public class ValueModelSerializer
    {
        public const string VALUE_MODEL_TYPE_PROPERTY_NAME = "ValueType";
        public const string VALUE_MODEL_VALUE_PROPERTY_NAME = "ValueData";

        private static ValueModelSerializerStrategyBase[] _strategies = new ValueModelSerializerStrategyBase[]
        {
            new ArrayModelSerializerStrategy(),
            new ObjectModelSerializerStrategy(),
            new PrimitiveModelSerializerStrategy(),
        };

        private ValueModelSerializerStrategyBase GetStrategy(Type type) => _strategies.FirstOrDefault(x => x.CanSerialize(type));

        public ValueModel Deserialize(JToken value)
        {
            return GetStrategy(JTokenToPropertyType(value)).Deserialize(value);
        }

        public JToken Serialize(ValueModel value)
            => GetStrategy(value.GetType()).Serialize(value);

        private Type JTokenToPropertyType (JToken token)
        {
            IsExplicit(token, out JToken data);

            if (data is JValue)
                return typeof(PrimitiveModel);

            if (data is JObject)
                return typeof(ObjectModel);

            if (data is JArray)
                return typeof(ArrayModel);

            return null;
        }

        public static bool IsExplicit (JToken token, out JToken valueData)
        {
            if (token is JObject obj &&
                obj.ContainsKey(VALUE_MODEL_TYPE_PROPERTY_NAME) && 
                obj.ContainsKey(VALUE_MODEL_TYPE_PROPERTY_NAME) && 
                obj.Count == 2)
            {
                valueData = obj[VALUE_MODEL_VALUE_PROPERTY_NAME];
                return true;
            }
            valueData = token;
            return false;
        }
    }
}
