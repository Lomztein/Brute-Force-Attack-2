using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.EngineObjectSerializers;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class EngineObjectPropertyAssembler : IPropertyAssembler
    {
        private static IEnumerable<IEngineObjectSerializer> _serializers;

        public EngineObjectPropertyAssembler ()
        {
            if (_serializers == null)
            {
                _serializers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IEngineObjectSerializer>();
            }
        }

        public object Assemble(JToken model, Type type)
        {
            return GetSerializer(type).Deserialize(model);
        }

        public JToken Dissassemble(object value, Type type)
        {
            return GetSerializer(type).Serialize(value);
        }

        private IEngineObjectSerializer GetSerializer (Type type)
        {
            return _serializers.First(x => x.CanConvert(type));
        }

        public bool Fits(Type type)
        {
            return _serializers.Any(x => x.CanConvert(type));
        }
    }
}
