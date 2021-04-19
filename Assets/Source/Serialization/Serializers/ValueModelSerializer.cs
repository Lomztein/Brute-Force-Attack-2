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
        public const string VALUE_MODEL_GUID_NAME = "$GUID"; // TODO: Create some sort of more easily extendable metadata handler.
        public const string VALUE_MODEL_TYPE_NAME = "$Type";
        public const string VALUE_MODEL_DATA_NAME = "$Data";

        private static ValueModelSerializerStrategyBase[] _strategies = new ValueModelSerializerStrategyBase[]
        {
            new NullModelSerializerStrategy(),
            new PrimitiveModelSerializerStrategy(),
            new ObjectModelSerializerStrategy(),
            new ArrayModelSerializerStrategy(),
        };

        private ValueModelSerializerStrategyBase GetStrategy(Type type) => _strategies.FirstOrDefault(x => x.CanSerialize(type));

        public ValueModel Deserialize(JToken value)
        {
            return GetStrategy(JTokenToPropertyType(value)).Deserialize(value);
        }

        public JToken Serialize(ValueModel value)
        {
            var strat = GetStrategy(value?.GetType());
            return strat == null ? JValue.CreateNull() : strat.Serialize(value);
        }

        private Type JTokenToPropertyType (JToken token)
        {
            HasMetadata(token, out JToken data);

            if (token == null)
                return typeof(NullModel);

            if (data is JValue value)
                return value.Type == JTokenType.Null ? typeof(NullModel) : typeof(PrimitiveModel);

            if (data is JObject)
                return typeof(ObjectModel);

            if (data is JArray)
                return typeof(ArrayModel);

            return null;
        }

        public static bool HasMetadata (JToken token, out JToken valueData)
        {
            if (token is JObject obj)
            {
                string[] explicitMarkers = new string[] { VALUE_MODEL_GUID_NAME, VALUE_MODEL_TYPE_NAME };
                if (explicitMarkers.Any(x => obj.ContainsKey(x)) && obj.ContainsKey(VALUE_MODEL_DATA_NAME))
                {
                    valueData = obj[VALUE_MODEL_DATA_NAME];
                    return true;
                }
            }
            valueData = token;
            return false;
        }
    }
}
