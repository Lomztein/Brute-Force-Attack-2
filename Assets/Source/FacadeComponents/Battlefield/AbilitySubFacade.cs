using Lomztein.BFA2.Abilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.FacadeComponents.Battlefield
{
    public class AbilitySubFacade : SceneFacadeSubComponent<BattlefieldFacade>
    {
        public AbilityManager Manager;

        public event Action<Ability, object> OnAbilityActivated;

        public override void OnSceneLoaded()
        {
            Manager = AbilityManager.Instance;
            Manager.OnAbilityActivated += OnAbilityActivated;
        }

        public override void OnSceneUnloaded()
        {
            if (Manager != null)
            {
                Manager.OnAbilityActivated += OnAbilityActivated;
            }
            Manager = null;
        }
    }
}
