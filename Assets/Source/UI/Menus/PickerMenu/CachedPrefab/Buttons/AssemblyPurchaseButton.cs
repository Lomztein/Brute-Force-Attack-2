using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Research;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI.ToolTip;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab.Buttons
{
    public class AssemblyPurchaseButton : UnlockablePurchaseButton
    {
        protected override bool IsUnlocked ()
        {
            if (_prefab.GetCache().TryGetComponent(out TurretAssembly assembly))
            {
                return assembly.GetComponents().All(x => UnlockList.IsUnlocked(x.Identifier));
            }
            return false;
        }
    }
}
