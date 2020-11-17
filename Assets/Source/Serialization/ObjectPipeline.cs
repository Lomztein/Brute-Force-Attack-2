using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Serializers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization
{
    public static class ObjectPipeline
    {
        public static T BuildObject<T>(JToken token)
            => (T)BuildObject(token, typeof(T));

        public static object BuildObject (JToken token, Type type)
        {
            ComplexModelSerializer serializer = new ComplexModelSerializer();
            ObjectAssembler assembler = new ObjectAssembler();

            var model = serializer.Deserialize(token);
            return assembler.Assemble(model, type);
        }

        public static JToken UnbuildObject (object obj)
        {
            ComplexModelSerializer serializer = new ComplexModelSerializer();
            ObjectAssembler assembler = new ObjectAssembler();

            return serializer.Serialize(assembler.Disassemble(obj));
        }
    }
}
