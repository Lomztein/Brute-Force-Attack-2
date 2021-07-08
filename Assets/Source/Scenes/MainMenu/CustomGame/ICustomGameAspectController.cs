using Lomztein.BFA2.Battlefield;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public interface ICustomGameAspectController
    {
        void ApplyBattlefieldSettings(BattlefieldSettings settings);
    }
}
