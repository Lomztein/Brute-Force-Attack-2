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
    public class ComplexPropertyModelSerializerStrategy : IPropertyModelSerializerStrategy
    {
        public bool CanSerialize(Type type) => type == typeof(ComplexPropertyModel);

        private ComplexModelSerializer _internalSerializer = new ComplexModelSerializer();

        public IPropertyModel Deserialize(JToken token)
        {
            return new ComplexPropertyModel(
                _internalSerializer.Deserialize(token)
                );
        }

        public JToken Serialize(IPropertyModel model)
        {
            ComplexPropertyModel objectModel = model as ComplexPropertyModel;
            return _internalSerializer.Serialize(objectModel.Model);
        }
    }
}
