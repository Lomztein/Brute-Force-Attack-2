using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers
{
    public interface ISerializer<T>
    {
        JToken Serialize(T value);

        T Deserialize(JToken value);
    }
}
