using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Assemblers.EngineObject;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class EngineObjectAssembler : IValueAssembler
    {
        private static IEnumerable<IEngineObjectAssembler> _serializers;

        public EngineObjectAssembler ()
        {
            if (_serializers == null)
            {
                _serializers = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IEngineObjectAssembler>();
            }
        }

        public object Assemble(ValueModel model, Type type, AssemblyContext context)
        {
            return GetAssembler(type).Assemble(model as ObjectModel, context);
        }

        public ValueModel Disassemble(object value, Type type, DisassemblyContext context)
        {
            return GetAssembler(value.GetType()).Disassemble(value, context);
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
