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
    public class PropertyModelSerializer : ISerializer<IPropertyModel>
    {
        private static IPropertyModelSerializerStrategy[] _strategies = new IPropertyModelSerializerStrategy[]
        {
            new ArrayPropertyModelSerializerStrategy(),
            new ComplexPropertyModelSerializerStrategy(),
            new PrimitivePropertyModelSerializerStrategy(),
        };

        private IPropertyModelSerializerStrategy GetStrategy(Type type) => _strategies.FirstOrDefault(x => x.CanSerialize(type));

        public IPropertyModel Deserialize(JToken value)
        {
            return GetStrategy(JTokenToPropertyType(value)).Deserialize(value);
        }

        public JToken Serialize(IPropertyModel value)
            => GetStrategy(value.GetType()).Serialize(value);

        private Type JTokenToPropertyType (JToken token)
        {
            if (token is JValue value)
                return typeof(PrimitivePropertyModel);

            if (token is JObject obj)
            {
                if (obj.ContainsKey("Array"))
                {
                    return typeof(ArrayPropertyModel);
                }
                else
                {
                    return typeof(ComplexPropertyModel);
                }
            }

            return null;
        }
    }
}
