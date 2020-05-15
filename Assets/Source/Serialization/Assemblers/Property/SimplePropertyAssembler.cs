using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class SimplePropertyAssembler : IPropertyAssembler
    {
        public bool Fits(Type obj)
        {
            return true;
        }

        public object Assemble(JToken model, Type obj)
        {
            return model.ToObject(obj);
        }

        public JToken Dissassemble(object value, Type type)
        {
            return JToken.FromObject(value);
        }
    }
}
