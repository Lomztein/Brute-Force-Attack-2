using System;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public interface IEngineObjectAssembler
    {
        bool CanConvert(Type objectType);
        object Assemble(ObjectModel value);
        ObjectModel Disassemble(object value);
    }
}