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
    // Stores the health of datastreams, datacores.
    public class HealthAssemblerPart : IBattlefieldAssemblerPart
    {
        public string Identifier => "Core.Health";

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            return new NullModel();
        }
    }
}
