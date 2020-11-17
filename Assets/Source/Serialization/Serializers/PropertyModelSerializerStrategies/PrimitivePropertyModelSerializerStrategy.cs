using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Serializers.PropertyModelSerializerStrategies
{
    public class PrimitivePropertyModelSerializerStrategy : PropertyModelSerializerStrategy
    {
        public override bool CanSerialize(Type type) => type == typeof(PrimitivePropertyModel);

        protected override PropertyModel DeserializeImplicit(JToken token)
        {
            return new PrimitivePropertyModel(token);
        }

        protected override JToken SerializeImplicit(PropertyModel model)
        {
            return (model as PrimitivePropertyModel).Value;
        }
    }
}
