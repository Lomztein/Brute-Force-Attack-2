using Lomztein.BFA2.Structures.Turrets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Stats.Elements
{
    public class ComplexityStatSheetElement : StatSheetElementBase
    {
        public override bool UpdateDisplay(GameObject target)
        {
            if (target.transform.root.TryGetComponent(out TurretAssembly assembly))
            {
                SetText(((assembly.Complexity + 1f) * 100f).ToString("0") + "% Complexity");
                return true;
            }
            return false;
        }
    }
}
