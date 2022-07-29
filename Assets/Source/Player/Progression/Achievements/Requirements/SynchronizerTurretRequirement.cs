using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Misc;
using Lomztein.BFA2.Structures.Turrets.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class SynchronizerTurretRequirement : AchievementRequirement
    {
        public override bool Binary => true;
        public override float Progression => RequirementsMet ? 1 : 0;
        public override bool RequirementsMet => _completed;
        private bool _completed;

        [ModelProperty]
        public string WeaponIdentifier;
        [ModelProperty]
        public int WeaponAmount;

        public override void End()
        {
            Facade.Battlefield.Structures.OnStructureAdded -= OnStructureAdded;
        }

        public override void Init()
        {
            Facade.Battlefield.Structures.OnStructureAdded += OnStructureAdded;
        }

        private void OnStructureAdded(Structures.Structure obj, object source)
        {
            TurretWeaponFireSynchronizer syncer = obj.GetComponentInChildren<TurretWeaponFireSynchronizer>();
            if (syncer)
            {
                bool allTrue = true;
                int amount = 0;
                foreach (TurretWeapon weapon in syncer.GetComponentsInChildren<TurretWeapon>())
                {
                    if (!weapon.Identifier.Contains(WeaponIdentifier))
                    {
                        allTrue = false;
                    }
                    else
                    {
                        amount++;
                    }
                }
                if (allTrue && amount == WeaponAmount)
                {
                    CheckRequirements();
                    _completed = true;
                }
            }
        }
    }
}
