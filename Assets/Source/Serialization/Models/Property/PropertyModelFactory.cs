using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Assemblers.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public static class PropertyModelFactory
    {
        public static PropertyModel Create (object value)
        {
            AllPropertyAssemblers assemblers = new AllPropertyAssemblers();
            return assemblers.Disassemble(value, value.GetType());
        }
    }
}
