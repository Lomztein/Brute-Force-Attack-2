using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Assemblers.EngineObject;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.Custom
{
    public class EngineObjectCustomAssembler : ICustomObjectAssembler
    {
        private static IEnumerable<IEngineObjectAssembler> _serializers;

        public EngineObjectCustomAssembler ()
        {
            if (_serializers == null)
            {
                _serializers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IEngineObjectAssembler>();
            }
        }

        public object Assemble(ObjectModel model, Type type)
        {
            return GetAssembler(type).Assemble(model);
        }

        public ObjectModel Disassemble(object value)
        {
            return GetAssembler(value.GetType()).Disassemble(value);
        }

        private IEngineObjectAssembler GetAssembler (Type type)
        {
            return _serializers.First(x => x.CanConvert(type));
        }

        public bool CanAssemble(Type type)
        {
            return _serializers.Any(x => x.CanConvert(type));
        }
    }
}
