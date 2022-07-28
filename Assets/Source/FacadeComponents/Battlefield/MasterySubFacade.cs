using Lomztein.BFA2.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.FacadeComponents.Battlefield
{
    public class MasterySubFacade : SceneFacadeSubComponent<BattlefieldFacade>
    {
        public event Action<int, object> OnMasteryModeLevelChanged;

        public override void OnSceneLoaded()
        {
            MasteryModeController.Instance.OnMasteryLevelChanged += Instance_OnMasteryLevelChanged;
        }

        private void Instance_OnMasteryLevelChanged(int arg1, object arg2)
        {
            OnMasteryModeLevelChanged?.Invoke(arg1, arg2);
        }

        public override void OnSceneUnloaded()
        {
            if (MasteryModeController.Instance)
            {
                MasteryModeController.Instance.OnMasteryLevelChanged -= Instance_OnMasteryLevelChanged;
            }
        }
    }
}
