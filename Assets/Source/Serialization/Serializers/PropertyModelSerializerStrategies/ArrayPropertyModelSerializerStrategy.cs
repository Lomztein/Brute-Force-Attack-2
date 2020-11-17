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
    public class ArrayPropertyModelSerializerStrategy : PropertyModelSerializerStrategy
    {
        public override bool CanSerialize(Type type) => type == typeof(ArrayPropertyModel);

        private PropertyModelSerializer _internalSerializer = new PropertyModelSerializer();

        protected override PropertyModel DeserializeImplicit(JToken token)
        {
            JArray array = token as JArray;
            List<PropertyModel> values = new List<PropertyModel>();

            foreach (var value in array)
            {
                values.Add(_internalSerializer.Deserialize(value));
            }

            return new ArrayPropertyModel(null, values.ToArray());
        }

        protected override JToken SerializeImplicit(PropertyModel model)
        {
            ArrayPropertyModel arrayModel = model as ArrayPropertyModel;
            return new JArray(arrayModel.Elements.Select(x => _internalSerializer.Serialize(x)));
        }
    }
}
