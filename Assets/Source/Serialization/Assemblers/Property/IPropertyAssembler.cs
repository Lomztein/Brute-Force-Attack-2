using System;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public interface IPropertyAssembler
    {
        object Assemble(IPropertyModel model, Type type);
        IPropertyModel Disassemble(object value, Type type);
        bool CanAssemble(Type type);
    }
}