using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Assemblers.EngineComponent
{
    public interface IEngineComponentAssembler
    {
        Type Type { get; }

        ObjectModel Disassemble(Component source);

        void Assemble(ObjectModel model, Component target);
    }
}
