using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class OrFilter : IModdableFilter
    {
        [ModelProperty, SerializeReference, SR]
        public IModdableFilter[] Filters;

        public bool Check(IModdable moddable)
        {
            return Filters.Any(x => x.Check(moddable));
        }
    }
}
