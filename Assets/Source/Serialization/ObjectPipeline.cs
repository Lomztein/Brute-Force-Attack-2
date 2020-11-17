using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
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
            ObjectAssembler assembler = new ObjectAssembler();
            return assembler.Assemble(DeserializeObject(token), type);
        }

        public static ObjectModel DeserializeObject (JToken token)
        {
            ComplexModelSerializer serializer = new ComplexModelSerializer();
            var model = serializer.Deserialize(token);
            return model;
        }

        public static JToken UnbuildObject (object obj)
        {
            ComplexModelSerializer serializer = new ComplexModelSerializer();
            ObjectAssembler assembler = new ObjectAssembler();

            return serializer.Serialize(assembler.Disassemble(obj));
        }

        public static JToken SerializeObject (ObjectModel model)
        {
            ComplexModelSerializer serializer = new ComplexModelSerializer();
            var token = serializer.Serialize(model);
            return token;
        }
    }
}
