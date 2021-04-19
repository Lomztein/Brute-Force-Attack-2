using Lomztein.BFA2.Serialization.Assemblers;
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

        public abstract void Assemble(ObjectModel model, T target, AssemblyContext context);

        public void Assemble(ObjectModel model, Component target, AssemblyContext context)
            => Assemble(model, target as T, context);

        public abstract ObjectModel Disassemble(T source, DisassemblyContext context);

        public ObjectModel Disassemble(Component source, DisassemblyContext context)
            => Disassemble(source as T, context);
    }
}
