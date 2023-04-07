using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Scenes.Battlefield.BattlefieldAssemblerParts
{
    public interface IBattlefieldAssemblerPart
    {
        public string Identifier { get; }

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context);

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context);
    }
}
