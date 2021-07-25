using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Modifiers
{
    public abstract class ModBase : Mod
    {
        public override float Coeffecient { get; set; }

        [ModelProperty]
        public ModdableAttribute[] RequiredAttributes;

        public override bool CanMod(IModdable moddable)
        {
            var attributes = moddable.GetModdableAttributes();
            return RequiredAttributes.All(x => attributes.Contains(x));
        }
    }
}
