using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class AssemblablePropertyAssembler : IPropertyAssembler
    {
        public object Assemble(PropertyModel model, Type type)
        {
            IAssemblable serializable = Activator.CreateInstance(type) as IAssemblable;
            serializable.Assemble((model as ComplexPropertyModel).Model);
            return serializable;
        }

        public PropertyModel Disassemble(object value, Type implicitType)
            => new ComplexPropertyModel((value as IAssemblable).Disassemble());

        public bool CanAssemble(Type type)
            => type.GetInterfaces().Contains(typeof(IAssemblable));
    }
}
