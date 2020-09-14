using System;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public interface IEngineObjectAssembler
    {
        bool CanConvert(Type objectType);
        object Assemble(IObjectModel value);
        IObjectModel Dissasemble(object value);
    }
}