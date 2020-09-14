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
    public class SerializablePropertyAssembler : IPropertyAssembler
    {
        public object Assemble(JToken model, Type type)
        {
            ISerializable serializable = Activator.CreateInstance(type) as ISerializable;
            serializable.Deserialize(model);
            return serializable;
        }

        public JToken Dissassemble(object value, Type type)
            => (value as ISerializable).Serialize();

        public bool CanAssemble(Type type)
            => type.GetInterfaces().Contains(typeof(ISerializable));
    }
}
