using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers
{
    [CreateAssetMenu(fileName = "NewStatMod", menuName = "BFA2/Mods/Composite Mod")]
    public class CompositeMod : Mod
    {
        [ModelProperty]
        public Mod[] InternalMods;

        public override void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            foreach (Mod mod in InternalMods)
            {
                mod.ApplyBase(stats, events);
            }
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            foreach (Mod mod in InternalMods)
            {
                mod.ApplyStack(stats, events);
            }
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            foreach (Mod mod in InternalMods)
            {
                mod.RemoveBase(stats, events);
            }
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            foreach (Mod mod in InternalMods)
            {
                mod.RemoveStack(stats, events);
            }
        }
    }
}
