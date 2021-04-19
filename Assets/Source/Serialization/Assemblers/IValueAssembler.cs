using System;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public interface IValueAssembler
    {
        object Assemble(ValueModel model, Type expectedType, AssemblyContext context);
        ValueModel Disassemble(object value, Type expectedType, DisassemblyContext context);
        bool CanAssemble(Type type);
    }
}