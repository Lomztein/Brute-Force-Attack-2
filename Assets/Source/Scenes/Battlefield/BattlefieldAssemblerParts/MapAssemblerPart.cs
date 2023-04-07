using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Scenes.Battlefield.BattlefieldAssemblerParts
{
    // Stores the current map in it's entirety, because honestly that's easier.
    // An obvious optimization is to only store the diff from the original map file, but I doubt that's neccesary.
    public class MapAssemblerPart : IBattlefieldAssemblerPart
    {
        public string Identifier => "Core.Map";

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
            MapData mapData = new MapData();
            mapData.Assemble(partData, context);
            controller.MapController.ApplyMapData(mapData);
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            return controller.MapController.MapData.Disassemble(context);
        }
    }
}
