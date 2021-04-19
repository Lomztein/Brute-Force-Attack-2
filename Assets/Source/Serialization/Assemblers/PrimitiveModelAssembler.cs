using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class PrimitiveModelAssembler : IValueAssembler
    {
        public bool CanAssemble(Type type) => IsPrimitive(type);

        public object Assemble(ValueModel model, Type obj, AssemblyContext context)
        {
            return (model as PrimitiveModel).ToObject(obj);
        }

        public ValueModel Disassemble(object value, Type type, DisassemblyContext context)
        {
            return new PrimitiveModel(value);
        }

        private static bool IsPrimitive(Type type)
            => type.IsPrimitive || type == typeof(string) || type.IsEnum;
    }
}
