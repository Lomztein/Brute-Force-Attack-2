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
    public class DefaultPropertyAssemblers : IPropertyAssembler
    {
        private IPropertyAssembler[] Assemblers => new IPropertyAssembler[]
        {
            new ArrayPropertyAssembler (),
            new SerializablePropertyAssembler(),
            new SimplePropertyAssembler(),
        };

        public object Assemble(JToken model, Type type)
        {
            return GetAssembler(type).Assemble(model, type);
        }

        public JToken Dissassemble(object obj, Type type)
        {
            return GetAssembler(type).Dissassemble(obj, type);
        }

        public bool Fits(Type type)
        {
            return GetAssembler(type) != null;
        }

        private IPropertyAssembler GetAssembler(Type type)
            => Assemblers.First(x => x.Fits(type));
    }
}
