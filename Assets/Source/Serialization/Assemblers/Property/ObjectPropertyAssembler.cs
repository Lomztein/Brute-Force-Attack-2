using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Property;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class ObjectPropertyAssembler : IPropertyAssembler
    {
        private ObjectAssembler _internalAssembler = new ObjectAssembler();

        public bool CanAssemble(Type type) => !type.IsPrimitive && type != typeof(string);

        public object Assemble(IPropertyModel model, Type type) => _internalAssembler.Assemble((model as ObjectPropertyModel).Model);

        public IPropertyModel Disassemble(object value, Type type) => new ObjectPropertyModel(_internalAssembler.Disassemble(value));
    }
}
