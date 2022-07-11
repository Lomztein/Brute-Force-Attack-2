using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Scenes.Battlefield;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class CustomGameMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            ApplyBattlefieldSettings(BattlefieldInitializeInfo.NewSettings);
        }

        public void ApplyBattlefieldSettings (BattlefieldSettings settings)
        {
            ICustomGameAspectController[] controllers = GetComponentsInChildren<ICustomGameAspectController>();
            foreach (var controller in controllers)
            {
                controller.ApplyBattlefieldSettings(settings);
            }
        }
    }
}
