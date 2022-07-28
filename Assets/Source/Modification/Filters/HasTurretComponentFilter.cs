using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Filters
{
    public class HasTurretComponentFilter : IModdableFilter
    {
        [ModelProperty]
        public string[] ApplicableComponentIdentifiers;

        public bool Check(IModdable moddable)
        {
            if (moddable is TurretAssembly assembly)
            {
                foreach (var turretComponent in assembly.GetComponents())
                {
                    if (ApplicableComponentIdentifiers.Any(x => turretComponent.Identifier.StartsWith(x)))
                    {
                        return true;
                    }
                }
                return false;
            }
            if (moddable is TurretComponent component)
            {
                return ApplicableComponentIdentifiers.Any(x => component.Identifier.StartsWith(x));
            }
            return false;
        }
    }
}
