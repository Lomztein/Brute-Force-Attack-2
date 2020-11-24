using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Assemblers.EngineComponent
{
    public abstract class EngineComponentAssembler<T> : IEngineComponentAssembler where T : Component
    {
        public Type Type => typeof(T);

        public abstract void Assemble(ObjectModel model, T target);

        public void Assemble(ObjectModel model, Component target)
            => Assemble(model, target as T);

        public abstract ObjectModel Disassemble(T source);

        public ObjectModel Disassemble(Component source)
            => Disassemble(source as T);
    }
}
