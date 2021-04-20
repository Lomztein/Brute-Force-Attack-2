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
        public abstract bool CanDeserialize(JToken token);

        private bool HasMetadata(JToken token) => ValueModelSerializer.HasMetadata(token, out JToken _);
        private bool HasMetadata(ValueModel model) => model == null ? false : model.HasMetadata;

        private JToken SerializeWithMetadata(ValueModel model)
            => CreateMetadataContainer(model, SerializeWithoutMetadata(model));

        protected abstract JToken SerializeWithoutMetadata(ValueModel model);

        private ValueModel DeserializeWithMetadata(JToken token)
        {
            JObject obj = token as JObject;
            ValueModel model = DeserializeWithoutMetadata(obj[ValueModelSerializer.VALUE_MODEL_DATA_NAME]);

            if (obj.TryGetValue (ValueModelSerializer.VALUE_MODEL_GUID_NAME, out JToken guid)) // TODO: Create some sort of more extensible metadata serialization handler
            {
                model.Guid = guid.ToObject<Guid>();
            }
            if (obj.TryGetValue(ValueModelSerializer.VALUE_MODEL_TYPE_NAME, out JToken type))
            {
                model.MakeExplicit(type.ToString());
            }
            
            return model;
        }

        protected abstract ValueModel DeserializeWithoutMetadata(JToken token);

        private JObject CreateMetadataContainer(ValueModel model, JToken data)
        {
            JObject obj = new JObject();

            if (model.HasGuid)
            {
                obj.Add(ValueModelSerializer.VALUE_MODEL_GUID_NAME, model.Guid);
            }
            if (!model.IsTypeImplicit)
            {
                obj.Add(ValueModelSerializer.VALUE_MODEL_TYPE_NAME, model.TypeName);
            }
            obj.Add(ValueModelSerializer.VALUE_MODEL_DATA_NAME, data);

            return obj;
        }


        public ValueModel Deserialize(JToken token)
            => HasMetadata(token) ? DeserializeWithMetadata(token) : DeserializeWithoutMetadata(token);

        public JToken Serialize(ValueModel model)
            => HasMetadata(model) ? SerializeWithMetadata(model) : SerializeWithoutMetadata(model);

    }
}
