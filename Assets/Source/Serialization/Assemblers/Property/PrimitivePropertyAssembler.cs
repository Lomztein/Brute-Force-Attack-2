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

        public object Assemble(PropertyModel model, Type obj)
        {
            return (model as PrimitivePropertyModel).ToObject(obj);
        }

        public PropertyModel Disassemble(object value, Type type)
        {
            return new PrimitivePropertyModel(value != null ? JToken.FromObject(value) : JValue.CreateNull());
        }

        private static bool IsPrimitive(Type type)
            => type.IsPrimitive || type == typeof(string) || type.IsEnum;
    }
}
