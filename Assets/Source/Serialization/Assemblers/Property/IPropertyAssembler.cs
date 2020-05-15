using System;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public interface IPropertyAssembler
    {
        object Assemble(JToken model, Type type);
        JToken Dissassemble(object value, Type type);
        bool Fits(Type type);
    }
}