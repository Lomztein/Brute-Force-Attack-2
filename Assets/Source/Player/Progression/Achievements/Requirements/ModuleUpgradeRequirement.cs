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
            int checkCount = 0;
            foreach (var structure in structures)
            {
                if (structure is TurretAssembly assembly)
                {
                    if (CheckAgainst.All(x => x.Check(structure)))
                    {
                        if (!CheckAssembly(assembly))
                        {
                            return false;
                        }
                        checkCount++;
                    }
                }
            }
            return checkCount > 0;
        }

        private bool CheckAssembly (TurretAssembly assembly)
        {
            if (ApplicableModuleIdentifiers.Length == 0)
            {
                return assembly.Modules.Count() > RequiredAmount;
            }
            else
            {
                return assembly.Modules.Count(x => ApplicableModuleIdentifiers.Contains(x.Item.Identifier)) > RequiredAmount;
            }
        }

        public override void End()
        {
            Facade.Battlefield.Structures.OnStructureAdded -= Structures_OnStructureAdded;
            Facade.Battlefield.Structures.OnStructureHierarchyChanged -= Structures_OnStructureHierarchyChanged;
        }

        public override void Init()
        {
            Facade.Battlefield.Structures.OnStructureAdded += Structures_OnStructureAdded;
            Facade.Battlefield.Structures.OnStructureHierarchyChanged += Structures_OnStructureHierarchyChanged;
        }

        private void Structures_OnStructureHierarchyChanged(Structures.Structure arg1, GameObject arg2, object arg3)
        {
            CheckProgress();
        }

        private void Structures_OnStructureAdded(Structures.Structure obj)
        {
            CheckProgress();
        }
    }
}
