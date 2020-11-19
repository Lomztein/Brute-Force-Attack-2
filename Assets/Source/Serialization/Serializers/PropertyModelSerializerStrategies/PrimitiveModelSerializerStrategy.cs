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
    public class PrimitiveModelSerializerStrategy : ValueModelSerializerStrategyBase
    {
        public override bool CanSerialize(Type type) => type == typeof(PrimitiveModel);

        protected override ValueModel DeserializeImplicit(JToken token)
        {
            return PrimitiveModel.FromToken(token);
        }

        protected override JToken SerializeImplicit(ValueModel model)
        {
            return (model as PrimitiveModel).Value;
        }
    }
}
