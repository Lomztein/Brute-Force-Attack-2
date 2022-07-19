using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class HasUnityComponentFilter : IModdableFilter
    {
        [ModelProperty]
        public string[] ApplicableComponentClassNames;
        [ModelProperty]
        public bool RequireAll;

        public bool Check(IModdable moddable)
        {
            if (moddable is Component component)
            {
                if (RequireAll)
                {
                    return ApplicableComponentClassNames.All(x => component.GetComponent(x) != null);
                }
                else
                {
                    return ApplicableComponentClassNames.Any(x => component.GetComponent(x) != null);
                }
            }
            return false;
        }
    }
}
