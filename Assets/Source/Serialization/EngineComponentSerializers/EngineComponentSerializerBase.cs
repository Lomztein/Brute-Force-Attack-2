using Lomztein.BFA2.Serialization.Models.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public abstract class EngineComponentSerializerBase<T> : IEngineComponentSerializer where T : Component
    {
        public Type Type => typeof(T);

        public abstract void Deserialize(IComponentModel model, T target);

        public void Deserialize(IComponentModel model, Component target)
            => Deserialize(model, target as T);

        public abstract IComponentModel Serialize(T source);

        public IComponentModel Serialize(Component source)
            => Serialize(source as T);
    }
}
