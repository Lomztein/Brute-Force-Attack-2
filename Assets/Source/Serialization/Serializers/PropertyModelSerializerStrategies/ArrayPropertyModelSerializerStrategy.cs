using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Serializers.PropertyModelSerializerStrategies
{
    public class ArrayPropertyModelSerializerStrategy : IPropertyModelSerializerStrategy
    {
        private PropertyModelSerializer _internalSerializer = new PropertyModelSerializer();

        public bool CanSerialize(Type type) => type.IsArray;

        public IPropertyModel Deserialize(JToken token, Type type)
        {
            JArray array = token["Array"] as JArray;
            List<IPropertyModel> values = new List<IPropertyModel>();
            foreach (var value in array)
            {
                values.Add(_internalSerializer.Deserialize(value));
            }
            return new ArrayPropertyModel(type, token["Name"].ToString(), values.ToArray());
        }

        public JToken Serialize(IPropertyModel model)
        {
            ArrayPropertyModel arrayModel = model as ArrayPropertyModel;
            JArray array = new JArray(arrayModel.Elements.Select(x => _internalSerializer.Serialize(x)));
            return new JObject()
            {
                { "Type", model.Type.FullName },
                { "Name", model.Name },
                { "Array", array }
            };
        }
    }
}
