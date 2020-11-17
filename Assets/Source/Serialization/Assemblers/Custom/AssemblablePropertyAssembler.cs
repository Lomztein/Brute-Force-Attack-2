using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.Custom
{
    public class AssemblableObjectCustomAssembler : ICustomObjectAssembler
    {
        public object Assemble(ObjectModel model, Type type)
        {
            IAssemblable serializable = Activator.CreateInstance(type) as IAssemblable;
            serializable.Assemble(new ComplexPropertyModel(model));
            return serializable;
        }

        public ObjectModel Disassemble(object value)
            => ((value as IAssemblable).Disassemble() as ComplexPropertyModel).Model;

        public bool CanAssemble(Type type)
            => type.GetInterfaces().Contains(typeof(IAssemblable));
    }
}
