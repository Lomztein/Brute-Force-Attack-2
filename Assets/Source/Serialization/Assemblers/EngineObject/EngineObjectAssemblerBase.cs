using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public abstract class EngineObjectAssemblerBase<T> : IEngineObjectAssembler
    {
        public bool CanConvert(Type objectType) => objectType == typeof(T);
        public object Assemble(ObjectModel value, AssemblyContext context)
        {
            return AssembleValue(value, context);
        }

        public abstract T AssembleValue(ObjectModel value, AssemblyContext context);

        public ObjectModel Disassemble(object value, DisassemblyContext context)
        {
            return DisassembleValue((T)value, context);
        }

        public abstract ObjectModel DisassembleValue(T value, DisassemblyContext context);
    }
}
