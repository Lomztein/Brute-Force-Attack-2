using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers
{
    public class CompositeMod : BaseModComponent, IMod
    {
        private static string _childName = "Modifiers";
        private IMod[] _internalMods;

        private void Awake()
        {
            _internalMods = transform.Find(_childName).GetComponents<IMod>();
        }

        public override void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in _internalMods)
            {
                mod.ApplyBase(stats, events);
            }
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in _internalMods)
            {
                mod.ApplyStack(stats, events);
            }
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in _internalMods)
            {
                mod.RemoveBase(stats, events);
            }
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in _internalMods)
            {
                mod.RemoveStack(stats, events);
            }
        }
    }
}
