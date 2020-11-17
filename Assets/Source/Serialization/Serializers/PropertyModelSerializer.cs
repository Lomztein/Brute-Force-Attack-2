using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Serialization.Serializers.PropertyModelSerializerStrategies;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers
{
    public class PropertyModelSerializer
    {
        public const string CS_TYPE_JSON_NAME = "-CSTypeFullName";

        private static PropertyModelSerializerStrategy[] _strategies = new PropertyModelSerializerStrategy[]
        {
            new ArrayPropertyModelSerializerStrategy(),
            new ComplexPropertyModelSerializerStrategy(),
            new PrimitivePropertyModelSerializerStrategy(),
        };

        private PropertyModelSerializerStrategy GetStrategy(Type type) => _strategies.FirstOrDefault(x => x.CanSerialize(type));

        public PropertyModel Deserialize(JToken value)
        {
            return GetStrategy(JTokenToPropertyType(value)).Deserialize(value);
        }

        public JToken Serialize(PropertyModel value)
            => GetStrategy(value.GetType()).Serialize(value);

        private Type JTokenToPropertyType (JToken token)
        {
            if (token is JObject obj && obj.ContainsKey(CS_TYPE_JSON_NAME))
                token = obj["Value"];

            if (token is JValue)
                return typeof(PrimitivePropertyModel);

            if (token is JObject)
                return typeof(ComplexPropertyModel);

            if (token is JArray)
                return typeof(ArrayPropertyModel);

            return null;
        }
    }
}
