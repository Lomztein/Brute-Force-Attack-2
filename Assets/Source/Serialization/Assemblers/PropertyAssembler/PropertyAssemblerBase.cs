using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lomztein.BFA2.Serialization.Assemblers.ObjectPopulator;

namespace Lomztein.BFA2.Serialization.Assemblers.PropertyAssembler
{
    public abstract class PropertyAssemblerBase
    {
        public abstract Type AttributeType { get; }

        public abstract void Disassemble(ObjectField field, object obj, Type expectedType, DisassemblyContext context);

        public abstract void Assemble(object obj, IAssignableMemberInfo member, ValueModel model, Type expectedType, AssemblyContext context);
    }
}
