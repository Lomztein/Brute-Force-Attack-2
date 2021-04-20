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

        // Ordering is important, as more than one strategy can handle a specific token type.
        // In other words, very specialized ones like Path and Reference must come before Primitive
        // as Primitive catches all JValue types that comes through, but the others only catch some JValue types.
        private static ValueModelSerializerStrategyBase[] _strategies = new ValueModelSerializerStrategyBase[]
        {
            new NullModelSerializerStrategy(),
            new PathModelSerializerStrategy(),
            new ReferenceModelSerializerStrategy(),
            new PrimitiveModelSerializerStrategy(),
            new ObjectModelSerializerStrategy(),
            new ArrayModelSerializerStrategy(),
        }; // TODO: Consider replacing with call to ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies

        private ValueModelSerializerStrategyBase GetSerializerStrategy(Type modelType)
            => _strategies.FirstOrDefault(x => x.CanSerialize(modelType));

        private ValueModelSerializerStrategyBase GetDeserialzierStrategy(JToken token)
            => _strategies.FirstOrDefault(x => x.CanDeserialize(token));

        public ValueModel Deserialize(JToken value)
        {
            HasMetadata(value, out JToken data);
            return GetDeserialzierStrategy(data).Deserialize(value);
        }

        public JToken Serialize(ValueModel value)
        {
            Type valueType = value == null ? typeof(NullModel) : value.GetType();
            var strat = GetSerializerStrategy(valueType);

            return strat == null ? JValue.CreateNull() : strat.Serialize(value);
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
