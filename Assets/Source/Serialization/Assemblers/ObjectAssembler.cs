using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class ObjectAssembler : IValueAssembler
    {
        private ObjectPopulator _populator = new ObjectPopulator();

        public object Assemble (ValueModel model, Type implicitType)
        {
            Type type = model.ValueType ?? implicitType;
            object obj = Activator.CreateInstance(type);
            _populator.Populate(obj, model as ObjectModel);
            return obj;
        }

        public bool CanAssemble(Type type) => IsComplex(type);

        public static bool IsComplex(Type type)
            => !type.IsPrimitive && type != typeof(string) && !type.IsEnum;

        public ValueModel Disassemble(object obj, Type type)
        {
            return _populator.Extract(obj);
        }
    }
}
