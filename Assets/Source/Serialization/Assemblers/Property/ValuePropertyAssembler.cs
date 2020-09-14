using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class ValuePropertyAssembler : IPropertyAssembler
    {
        public bool CanAssemble(Type type) => type.IsPrimitive || type == typeof(string);

        public object Assemble(IPropertyModel model, Type obj)
        {
            return (model as ValuePropertyModel).Value;
        }

        public IPropertyModel Disassemble(object value, Type type)
        {
            return new ValuePropertyModel(value);
        }
    }
}
