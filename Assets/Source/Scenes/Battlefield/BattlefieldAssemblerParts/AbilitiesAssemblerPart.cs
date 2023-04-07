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
    // Stores the cooldowns and such of abilities. Should not store abilities themselves, as they are added by other mechanics.
    public class AbilitiesAssemblerPart : IBattlefieldAssemblerPart
    {
        public string Identifier => "Core.Abilities";

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            return new NullModel();
        }
    }
}
