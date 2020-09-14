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
        public object Assemble(IObjectModel value)
        {
            return AssembleValue(value);
        }

        public abstract T AssembleValue(IObjectModel value);

        public IObjectModel Dissasemble(object value)
        {
            return DissasembleValue((T)value);
        }

        public abstract IObjectModel DissasembleValue(T value);
    }
}
