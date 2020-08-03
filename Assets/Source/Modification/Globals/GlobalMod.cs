using Lomztein.BFA2.Modification.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Globals
{
    public class GlobalMod
    {
        public GlobalMod(Predicate<IModdable> filter, string targetType, IMod mod)
        {
            Filter = filter;
            TargetType = targetType;
            Mod = mod;
        }

        public Predicate<IModdable> Filter;
        public string TargetType;
        public IMod Mod { get; private set; }
    }
}
