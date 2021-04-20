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
    public class ArrayModelSerializerStrategy : ValueModelSerializerStrategyBase
    {
        public override bool CanSerialize(Type type) => type == typeof(ArrayModel);
        public override bool CanDeserialize(JToken token) => token is JArray;

        private ValueModelSerializer _internalSerializer = new ValueModelSerializer();

        protected override ValueModel DeserializeWithoutMetadata(JToken token)
        {
            JArray array = token as JArray;
            List<ValueModel> values = new List<ValueModel>();

            foreach (var value in array)
            {
                values.Add(_internalSerializer.Deserialize(value));
            }

            return new ArrayModel(values.ToArray());
        }

        protected override JToken SerializeWithoutMetadata(ValueModel model)
        {
            ArrayModel arrayModel = model as ArrayModel;
            return new JArray(arrayModel.Elements.Select(x => _internalSerializer.Serialize(x)));
        }
    }
}
