using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Serializers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization
{
    public static class ObjectPipeline
    {
        public static T BuildObject<T>(JToken json)
        {
            ComplexModelSerializer serializer = new ComplexModelSerializer();
            ObjectAssembler assembler = new ObjectAssembler();

            return (T)assembler.Assemble(serializer.Deserialize(json));
        }

        public static JToken UnbuildObject (object obj)
        {
            ComplexModelSerializer serializer = new ComplexModelSerializer();
            ObjectAssembler assembler = new ObjectAssembler();

            return serializer.Serialize(assembler.Disassemble(obj));
        }
    }
}
