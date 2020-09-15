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
    public class PrimitivePropertyAssembler : IPropertyAssembler
    {
        public bool CanAssemble(Type type) => IsPrimitive(type);

        public object Assemble(IPropertyModel model, Type obj)
        {
            return (model as PrimitivePropertyModel).ToObject(obj);
        }

        public IPropertyModel Disassemble(object value)
        {
            return new PrimitivePropertyModel(JToken.FromObject(value));
        }
        private static bool IsPrimitive(Type type)
            => type.IsPrimitive || type == typeof(string) || type.IsEnum;
    }
}
