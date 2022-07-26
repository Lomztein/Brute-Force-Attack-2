using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class GameObjectAssembler : IValueAssembler
    {
        // This relationship should probably be the other way around, this feels wrong.
        private ContentSystem.Assemblers.GameObjectAssembler _assembler = new ContentSystem.Assemblers.GameObjectAssembler();

        public object Assemble(ValueModel model, Type expectedType, AssemblyContext context)
        {
            return _assembler.Assemble(model as ObjectModel, context);
        }

        public bool CanAssemble(Type type)
        {
            return type == typeof(GameObject);
        }

        public ValueModel Disassemble(object value, Type expectedType, DisassemblyContext context)
        {
            return _assembler.Disassemble(value as GameObject, context);
        }
    }
}
