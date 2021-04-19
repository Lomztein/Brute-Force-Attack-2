using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers.ModelSerializerStrategies
{
    class NullModelSerializerStrategy : ValueModelSerializerStrategyBase
    {
        public override bool CanSerialize(Type type)
            => type == typeof(NullModel);

        protected override ValueModel DeserializeWithoutMetadata(JToken token) => new NullModel();

        protected override JToken SerializeWithoutMetadata(ValueModel model) => JValue.CreateNull();
    }
}
