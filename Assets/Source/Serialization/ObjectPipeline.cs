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
            RootAssembler assembler = new RootAssembler();
            return assembler.Assemble(DeserializeObject(token), type);
        }

        public static RootModel DeserializeObject (JToken token)
        {
            RootSerializer serializer = new RootSerializer();
            var model = serializer.Deserialize(token);
            return model;
        }

        public static JToken UnbuildObject (object obj, bool implicitType = false)
        {
            RootSerializer serializer = new RootSerializer();
            RootAssembler assembler = new RootAssembler();
            var model = assembler.Disassemble(obj, !implicitType);
            return serializer.Serialize(model);
        }

        public static JToken SerializeObject (RootModel model)
        {
            RootSerializer serializer = new RootSerializer();
            var token = serializer.Serialize(model);
            return token;
        }
    }
}
