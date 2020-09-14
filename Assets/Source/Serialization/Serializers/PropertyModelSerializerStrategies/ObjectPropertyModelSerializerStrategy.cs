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
    public class ObjectPropertyModelSerializerStrategy : IPropertyModelSerializerStrategy
    {
        public bool CanSerialize(Type type) => !type.IsPrimitive && type != typeof (string);

        private ObjectModelSerializer _internalSerializer = new ObjectModelSerializer();

        public IPropertyModel Deserialize(JToken token, Type type)
        {
            return new ObjectPropertyModel(
                _internalSerializer.Deserialize(token["Object"])
                );
        }

        public JToken Serialize(IPropertyModel model)
        {
            ObjectPropertyModel objectModel = model as ObjectPropertyModel;

            return new JObject()
            {
                {"Type", model.Type.FullName },
                {"Object", _internalSerializer.Serialize (objectModel.Model) }
            };
        }
    }
}
