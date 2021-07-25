using Lomztein.BFA2.Modification.Filters;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Modification.Modifiers
{
    public abstract class ModBase : Mod
    {
        public override float Coeffecient { get; set; }

        [ModelProperty, SerializeReference, SR]
        public IModdableFilter[] Filters;

        public override bool CanMod(IModdable moddable)
        {
            return Filters.All(x => x.Check(moddable));
        }
    }
}
