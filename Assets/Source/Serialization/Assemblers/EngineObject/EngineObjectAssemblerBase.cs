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
        public object Assemble(ObjectModel value)
        {
            return AssembleValue(value);
        }

        public abstract T AssembleValue(ObjectModel value);

        public ObjectModel Disassemble(object value)
        {
            return DisassembleValue((T)value);
        }

        public abstract ObjectModel DisassembleValue(T value);
    }
}
