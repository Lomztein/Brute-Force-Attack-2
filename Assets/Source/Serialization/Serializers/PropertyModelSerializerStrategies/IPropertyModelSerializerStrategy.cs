using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers.PropertyModelSerializerStrategies
{
    public interface IPropertyModelSerializerStrategy
    {
        bool CanSerialize(Type type);

        IPropertyModel Deserialize(JToken token);

        JToken Serialize(IPropertyModel model);
    }
}
