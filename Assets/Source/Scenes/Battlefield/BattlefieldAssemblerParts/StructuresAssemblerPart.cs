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
    // Stores what structures have been placed on the map and whatever data they contain such as settings and modules.
    public class StructuresAssemblerPart : IBattlefieldAssemblerPart
    {
        public string Identifier => "Core.Structures";

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            return new NullModel();
        }
    }
}
