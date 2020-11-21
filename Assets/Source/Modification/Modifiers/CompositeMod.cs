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
    public class CompositeMod : IMod
    {
        [ModelProperty]
        [SerializeField]
        protected string _identifier;
        public string Identifier => _identifier;

        [ModelProperty]
        [SerializeField]
        protected string _name;
        public string Name => _name;

        [ModelProperty]
        [SerializeField]
        protected string _description;
        public string Description => _description;

        [ModelProperty]
        public IMod[] InternalMods;

        public void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in InternalMods)
            {
                mod.ApplyBase(stats, events);
            }
        }

        public void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in InternalMods)
            {
                mod.ApplyStack(stats, events);
            }
        }

        public void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in InternalMods)
            {
                mod.RemoveBase(stats, events);
            }
        }

        public void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in InternalMods)
            {
                mod.RemoveStack(stats, events);
            }
        }

        public bool IsCompatableWith(IModdable moddable)
        {
            return InternalMods.All(x => x.IsCompatableWith(moddable));
        }
    }
}
