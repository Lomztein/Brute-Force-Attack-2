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
    // Stores information relating to waves, which is really just which wave we're on lol.
    public class WaveAssemblerPart : IBattlefieldAssemblerPart
    {
        public string Identifier => "Core.Wave";
        public int AssemblyOrder => 40;

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
            controller.RoundController.SetWave((partData as PrimitiveModel).ToObject<int>());
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            return new PrimitiveModel(controller.RoundController.NextIndex);
        }
    }
}
