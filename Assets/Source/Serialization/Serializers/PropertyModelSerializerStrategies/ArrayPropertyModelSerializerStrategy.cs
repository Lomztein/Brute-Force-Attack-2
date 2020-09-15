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
    public class ArrayPropertyModelSerializerStrategy : IPropertyModelSerializerStrategy
    {
        private PropertyModelSerializer _internalSerializer = new PropertyModelSerializer();

        public bool CanSerialize(Type type) => type == typeof(ArrayPropertyModel);

        public IPropertyModel Deserialize(JToken token)
        {
            JArray array = token["Array"] as JArray;
            Type type = ReflectionUtils.GetType(token["Type"].ToString());
            List<IPropertyModel> values = new List<IPropertyModel>();

            foreach (var value in array)
            {
                values.Add(_internalSerializer.Deserialize(value));
            }
            return new ArrayPropertyModel(type, values.ToArray());
        }

        public JToken Serialize(IPropertyModel model)
        {
            ArrayPropertyModel arrayModel = model as ArrayPropertyModel;

            JArray array = new JArray(arrayModel.Elements.Select(x => _internalSerializer.Serialize(x)));
            return new JObject()
            {
                { "Type", arrayModel.Type.FullName },
                { "Array", array }
            };
        }
    }
}
