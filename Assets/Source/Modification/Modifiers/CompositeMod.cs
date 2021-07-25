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
        public override float Coeffecient { get => _coeffecient; set => SetCoeffecient(value); }
        private float _coeffecient;

        private void SetCoeffecient(float value)
        {
            _coeffecient = value;
            foreach (Mod mod in InternalMods)
            {
                mod.Coeffecient = _coeffecient;
            }
        }

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

        public override bool CanMod(IModdable moddable) => InternalMods.All(x => x.CanMod(moddable));
     }
}
