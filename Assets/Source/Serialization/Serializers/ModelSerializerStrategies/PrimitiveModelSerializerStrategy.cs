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
        public override bool CanDeserialize(JToken token) => token is JValue;

        protected override ValueModel DeserializeWithoutMetadata(JToken token)
        {
            return PrimitiveModel.FromToken(token as JValue);
        }

        protected override JToken SerializeWithoutMetadata(ValueModel model)
        {
            PrimitiveModel prim = model as PrimitiveModel;
            if (prim.Value == null || prim.StoreAs == null)
            {
                return JValue.CreateNull();
            }
            return new JValue (Convert.ChangeType(prim.Value, prim.StoreAs));
        }
    }
}
