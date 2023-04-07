using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Research;
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
        public int AssemblyOrder => 30;

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
            ResearchController researchController = ResearchController.Instance;
            ArrayModel array = partData as ArrayModel;
            foreach (ValueModel model in array)
            {
                PrimitiveModel pModel = model as PrimitiveModel;
                var option = researchController.GetOption(pModel.ToObject<string>());
                researchController.BeginResearch(option);
                researchController.ForceCompleteResearch(option);
            }
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            ResearchController researchController = ResearchController.Instance;
            return new ArrayModel(researchController.GetCompleted().Select(x => new PrimitiveModel(x.Identifier)));
        }
    }
}
