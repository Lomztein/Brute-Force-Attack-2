using Lomztein.BFA2.Modification.Filters;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.StructureManagement;
using Lomztein.BFA2.Structures.Turrets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class ModuleUpgradeRequirement : AchievementRequirement
    {
        public override bool Binary => true;
        public override float Progression => BinaryProgression();
        public override bool RequirementsMet => CheckRequirement();

        [ModelProperty, SerializeReference, SR]
        public IModdableFilter[] CheckAgainst;
        [ModelProperty]
        public string[] ApplicableModuleIdentifiers; // Empty if any.
        [ModelProperty]
        public int RequiredAmount;

        private bool CheckRequirement ()
        {
            var structures = StructureManager.GetStructures();
            foreach (var structure in structures)
            {
                if (structure is TurretAssembly assembly)
                {
                    if (!CheckAgainst.Any() || CheckAgainst.All(x => x.Check(structure)))
                    {
                        return CheckAssembly(assembly);
                    }
                }
            }
            return false;
        }

        private bool CheckAssembly (TurretAssembly assembly)
        {
            if (ApplicableModuleIdentifiers.Length == 0)
            {
                return assembly.Modules.Count() >= RequiredAmount;
            }
            else
            {
                int count = assembly.Modules.Count(x => ApplicableModuleIdentifiers.Contains(x.Item.Identifier));
                return count >= RequiredAmount;
            }
        }

        public override void End()
        {
            Facade.Battlefield.Structures.OnStructureStatChanged -= Structures_OnStructureStatChanged;
            Facade.Battlefield.Structures.OnStructureEventChanged -= Structures_OnStructureEventChanged;
        }

        public override void Init()
        {
            Facade.Battlefield.Structures.OnStructureStatChanged += Structures_OnStructureStatChanged;
            Facade.Battlefield.Structures.OnStructureEventChanged += Structures_OnStructureEventChanged;
        }

        private void Structures_OnStructureEventChanged(Structures.Structure arg1, Modification.Events.IEventReference arg2, object arg3)
        {
            CheckProgress();
        }

        private void Structures_OnStructureStatChanged(Structures.Structure arg1, Modification.Stats.IStatReference arg2, object arg3)
        {
            CheckProgress();
        }
    }
}
