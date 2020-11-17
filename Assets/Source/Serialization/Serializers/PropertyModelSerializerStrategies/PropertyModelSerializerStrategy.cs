using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers.PropertyModelSerializerStrategies
{
    public abstract class PropertyModelSerializerStrategy
    {
        public abstract bool CanSerialize(Type type);

        private bool IsImplicit(JToken token) => !(token is JObject obj && obj.ContainsKey(PropertyModelSerializer.CS_TYPE_JSON_NAME));
        private bool IsImplicit(PropertyModel model) => model.ImplicitType;

        private JToken SerializeExplicit(PropertyModel model)
            => CreateExplicitContainer(model.PropertyType, SerializeImplicit(model));


        private PropertyModel DeserializeExplicit(JToken token)
        {
            (Type type, JToken value) = GetDataFromExplicitContainer(token);
            PropertyModel model = DeserializeImplicit(value);
            model.PropertyType = type;
            return model;
        }

        private JObject CreateExplicitContainer(Type type, JToken value)
        {
            return new JObject()
            {
                { PropertyModelSerializer.CS_TYPE_JSON_NAME, type.FullName },
                { "Value", value }
            };
        }

        private (Type, JToken value) GetDataFromExplicitContainer(JToken container)
        {
            return (ReflectionUtils.GetType(container[PropertyModelSerializer.CS_TYPE_JSON_NAME].ToString()), container["Value"]);
        }

        protected abstract JToken SerializeImplicit(PropertyModel model);
        protected abstract PropertyModel DeserializeImplicit(JToken token);

        public PropertyModel Deserialize(JToken token)
            => IsImplicit(token) ? DeserializeImplicit(token) : DeserializeExplicit(token);

        public JToken Serialize(PropertyModel model)
            => IsImplicit(model) ? SerializeImplicit(model) : SerializeExplicit(model);

    }
}
