using Lomztein.BFA2.Abilities;
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
        public int AssemblyOrder => 100;

        public void AssemblePart(BattlefieldController controller, ValueModel partData, AssemblyContext context)
        {
            AbilityManager manager = AbilityManager.Instance;
            ObjectModel model = partData as ObjectModel;
            foreach (var abilityData in model)
            {
                Ability ability = manager.GetAbility(abilityData.Name);
                ability.AssembleData(abilityData.Model);
            }
        }

        public ValueModel DisassemblePart(BattlefieldController controller, DisassemblyContext context)
        {
            AbilityManager manager = AbilityManager.Instance;
            var fields = new List<ObjectField>();
            foreach (var ability in manager.CurrentAbilities)
            {
                var data = ability.DisassembleData();
                if (!ValueModel.IsNull(data))
                {
                    fields.Add(new ObjectField(ability.Identifier, data));
                }
            }
            return new ObjectModel(fields.ToArray());
        }
    }
}
