using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers.ModelSerializerStrategies
{
    public abstract class ValueModelSerializerStrategyBase
    {
        public abstract bool CanSerialize(Type type);

        private bool IsImplicit(JToken token) => !ValueModelSerializer.IsExplicit(token, out JToken _);
        private bool IsImplicit(ValueModel model) => model.IsTypeImplicit;

        private JToken SerializeExplicit(ValueModel model)
            => CreateExplicitContainer(model.GetModelType(), SerializeImplicit(model));


        private ValueModel DeserializeExplicit(JToken token)
        {
            (Type type, JToken value) = GetDataFromExplicitContainer(token);
            ValueModel model = DeserializeImplicit(value);
            model.MakeExplicit(type);
            return model;
        }

        private JObject CreateExplicitContainer(Type type, JToken value)
        {
            return new JObject()
            {
                { ValueModelSerializer.VALUE_MODEL_TYPE_PROPERTY_NAME, type.FullName },
                { ValueModelSerializer.VALUE_MODEL_VALUE_PROPERTY_NAME, value }
            };
        }

        private (Type, JToken value) GetDataFromExplicitContainer(JToken container)
        {
            return (ReflectionUtils.GetType(container[ValueModelSerializer.VALUE_MODEL_TYPE_PROPERTY_NAME].ToString()), container[ValueModelSerializer.VALUE_MODEL_VALUE_PROPERTY_NAME]);
        }

        protected abstract JToken SerializeImplicit(ValueModel model);
        protected abstract ValueModel DeserializeImplicit(JToken token);

        public ValueModel Deserialize(JToken token)
            => IsImplicit(token) ? DeserializeImplicit(token) : DeserializeExplicit(token);

        public JToken Serialize(ValueModel model)
            => IsImplicit(model) ? SerializeImplicit(model) : SerializeExplicit(model);

    }
}
