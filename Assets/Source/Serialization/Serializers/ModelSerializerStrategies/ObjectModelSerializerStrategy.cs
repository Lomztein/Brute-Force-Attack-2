using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Serializers.ModelSerializerStrategies
{
    public class ObjectModelSerializerStrategy : ValueModelSerializerStrategyBase
    {
        public override bool CanSerialize(Type type) => type == typeof(ObjectModel);
        private ValueModelSerializer _internalSerializer = new ValueModelSerializer();

        protected override ValueModel DeserializeImplicit(JToken value)
        {
            List<ObjectField> properties = new List<ObjectField>();

            JToken jProps = value;
            foreach (JProperty property in jProps)
            {
                properties.Add(new ObjectField(property.Name, _internalSerializer.Deserialize(property.Value)));
            }

            return new ObjectModel(properties.ToArray());
        }

        protected override JToken SerializeImplicit(ValueModel value)
        {
            ObjectModel model = value as ObjectModel;
            IEnumerable<JProperty> properties = model.GetProperties().Select(x => new JProperty(x.Name, _internalSerializer.Serialize(x.Model)));
            return new JObject(properties);
        }
    }
}
