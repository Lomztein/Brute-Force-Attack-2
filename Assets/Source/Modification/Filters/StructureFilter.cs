using Lomztein.BFA2.Structures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public abstract class StructureFilter : IModdableFilter
    {
        public bool Check(IModdable moddable)
        {
            if (moddable is Structure structure)
            {
                return Check(structure);
            }
            return false;
        }

        public abstract bool Check(Structure structure);
    }
}