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
    // Stores what research has been researched, and the progress of ongoing research.
    public class ResearchAssemblerPart : IBattlefieldAssemblerPart
    {
        public string Identifier => "Core.Research";

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            return new NullModel();
        }
    }
}
