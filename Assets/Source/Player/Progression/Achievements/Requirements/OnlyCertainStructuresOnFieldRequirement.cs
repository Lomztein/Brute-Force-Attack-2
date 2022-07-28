using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Modification.Filters;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.StructureManagement;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Structures.Turrets.Weapons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class OnlyCertainStructuresOnFieldRequirement : AchievementRequirement
    {
        public override bool Binary => true;
        public override float Progression => RequirementsMet ? 1f : 0f;
        public override bool RequirementsMet => CheckField();

        [ModelProperty]
        public bool PermaFailUntillNewGame;

        [ModelProperty, SerializeReference, SR]
        IModdableFilter[] CheckAgainst;
        [ModelProperty, SerializeReference, SR]
        IModdableFilter[] MustPass; // Not great to pull structure filters all the way in here, but /shrug

        private bool _failedOnce;

        private bool CheckField ()
        {
            if (StructureManager.Instance != null)
            {
                var structures = StructureManager.GetStructures();
                foreach (var structuresItem in structures)
                {
                    if (!CheckStructure(structuresItem))
                    {
                        if (PermaFailUntillNewGame)
                        {
                            _failedOnce = true;
                        }
                        return false;
                    }
                }
                if (PermaFailUntillNewGame && _failedOnce)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckStructure (Structure structure)
        {
            if (CheckAgainst.Any() && CheckAgainst.All(x => x.Check(structure)))
            {
                return MustPass.All(x => x.Check(structure));
            }
            else
            {
                return true;
            }
        }

        public override void End()
        {
            Facade.Battlefield.Structures.OnStructureAdded -= Structures_OnStructureAdded;
            Facade.Battlefield.Structures.OnStructureHierarchyChanged -= Structures_OnStructureHierarchyChanged;
            Facade.Battlefield.OnSceneLoaded -= Battlefield_OnSceneLoaded;
        }

        public override void Init()
        {
            Facade.Battlefield.Structures.OnStructureAdded += Structures_OnStructureAdded;
            Facade.Battlefield.Structures.OnStructureHierarchyChanged += Structures_OnStructureHierarchyChanged;
            Facade.Battlefield.OnSceneLoaded += Battlefield_OnSceneLoaded;
        }

        private void Battlefield_OnSceneLoaded()
        {
            _failedOnce = false;
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
