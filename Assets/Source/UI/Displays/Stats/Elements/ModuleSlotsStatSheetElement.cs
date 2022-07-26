using Lomztein.BFA2.Structures.Turrets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays.Stats.Elements
{
    public class ModuleSlotsStatSheetElement : StatSheetElementBase
    {
        public override bool UpdateDisplay(GameObject target)
        {
            if (target.transform.root.TryGetComponent(out TurretAssembly assembly))
            {
                SetText(assembly.FreeModuleSlots() + " Module Slots");
                return true;
            }
            return false;
        }
    }
}
