using System;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public interface IValueAssembler
    {
        object Assemble(ValueModel model, Type type);
        ValueModel Disassemble(object value, Type type);
        bool CanAssemble(Type type);
    }
}