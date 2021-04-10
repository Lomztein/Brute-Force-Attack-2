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
            return PrimitiveModel.FromToken(token as JValue);
        }

        protected override JToken SerializeImplicit(ValueModel model)
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
