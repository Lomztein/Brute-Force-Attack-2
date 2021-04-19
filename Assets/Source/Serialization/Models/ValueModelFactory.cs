using Lomztein.BFA2.Serialization.Assemblers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models
{
    public static class ValueModelFactory
    {
        public static ValueModel Create (object value, DisassemblyContext context)
        {
            ValueAssembler assemblers = new ValueAssembler();
            return assemblers.Disassemble(value, value.GetType(), context);
        }
    }
}
