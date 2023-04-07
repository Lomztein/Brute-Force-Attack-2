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
        public int AssemblyOrder => 20;

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
            float health = (partData as PrimitiveModel).ToObject<float>();
            float diff = health - Player.Player.Health.GetCurrentHealth();
            Player.Player.Health.ChangeHealth(diff, this);
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            return new PrimitiveModel(Player.Player.Health.GetCurrentHealth());
        }
    }
}
