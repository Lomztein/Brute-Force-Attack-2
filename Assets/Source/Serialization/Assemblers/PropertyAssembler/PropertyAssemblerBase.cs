using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers.PropertyAssembler
{
    public abstract class PropertyAssemblerBase
    {
        public abstract Type AttributeType { get; }

        public abstract void Disassemble(ObjectField field, object obj, Type expectedType, DisassemblyContext context);

        public abstract object Assemble(ValueModel model, Type expectedType, AssemblyContext context);
    }
}
