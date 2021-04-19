using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class AssemblableAssembler : IValueAssembler
    {
        public object Assemble(ValueModel model, Type expectedType, AssemblyContext context)
        {
            IAssemblable serializable = Activator.CreateInstance(expectedType) as IAssemblable;
            serializable.Assemble(model, context);
            return serializable;
        }

        public ValueModel Disassemble(object value, Type expectedType, DisassemblyContext context)
            => (value as IAssemblable).Disassemble(context);

        public bool CanAssemble(Type type)
            => type.GetInterfaces().Contains(typeof(IAssemblable));
    }
}
