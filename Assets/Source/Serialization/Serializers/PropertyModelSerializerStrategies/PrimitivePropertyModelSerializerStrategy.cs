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
    public class PrimitivePropertyModelSerializerStrategy : IPropertyModelSerializerStrategy
    {
        public bool CanSerialize(Type type) => type == typeof(PrimitivePropertyModel);

        public IPropertyModel Deserialize(JToken token)
        {
            return new PrimitivePropertyModel(token);
        }

        public JToken Serialize(IPropertyModel model)
        {
            PrimitivePropertyModel valueModel = model as PrimitivePropertyModel;
            return valueModel.Value;
        }
    }
}
