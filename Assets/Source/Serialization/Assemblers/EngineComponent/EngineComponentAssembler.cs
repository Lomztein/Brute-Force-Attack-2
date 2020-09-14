using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineComponent
{
    public abstract class EngineComponentAssembler<T> : IEngineComponentAssembler where T : Component
    {
        public Type Type => typeof(T);

        public abstract void Assemble(IObjectModel model, T target);

        public void Assemble(IObjectModel model, Component target)
            => Assemble(model, target as T);

        public abstract IObjectModel Disassemble(T source);

        public IObjectModel Disassemble(Component source)
            => Disassemble(source as T);
    }
}
