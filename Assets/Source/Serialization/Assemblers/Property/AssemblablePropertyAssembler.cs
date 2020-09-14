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
        public object Assemble(IPropertyModel model, Type type)
        {
            IAssemblable serializable = Activator.CreateInstance(type) as IAssemblable;
            serializable.Assemble((model as ObjectPropertyModel).Model);
            return serializable;
        }

        public IPropertyModel Disassemble(object value, Type type)
            => new ObjectPropertyModel((value as IAssemblable).Disassemble());

        public bool CanAssemble(Type type)
            => type.GetInterfaces().Contains(typeof(IAssemblable));
    }
}
