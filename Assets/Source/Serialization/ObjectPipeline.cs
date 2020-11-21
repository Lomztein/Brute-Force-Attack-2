﻿using Lomztein.BFA2.Serialization.Assemblers;
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
            ValueAssembler assembler = new ValueAssembler();
            return assembler.Assemble(DeserializeObject(token), type);
        }

        public static ValueModel DeserializeObject (JToken token)
        {
            ValueModelSerializer serializer = new ValueModelSerializer();
            var model = serializer.Deserialize(token);
            return model;
        }

        public static JToken UnbuildObject (object obj, bool implicitType = false)
        {
            ValueModelSerializer serializer = new ValueModelSerializer();
            ValueAssembler assembler = new ValueAssembler();

            var model = assembler.Disassemble(obj, typeof(object));
            if (implicitType)
                model.MakeImplicit();

            return serializer.Serialize(model);
        }

        public static JToken SerializeObject (ObjectModel model)
        {
            ValueModelSerializer serializer = new ValueModelSerializer();
            var token = serializer.Serialize(model);
            return token;
        }
    }
}
