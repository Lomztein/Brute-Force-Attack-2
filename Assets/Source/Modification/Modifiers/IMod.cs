using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers
{
    public interface IMod
    {
        string Identifier { get; }
        string Name { get; }
        string Description { get; }
        ModdableAttribute[] RequiredAttributes { get; }


        void ApplyBase(IStatContainer stats, IEventContainer events);
        void ApplyStack(IStatContainer stats, IEventContainer events);

        void RemoveBase(IStatContainer stats, IEventContainer events);
        void RemoveStack(IStatContainer stats, IEventContainer events);
    }

    public static class ModExtensions
    {
        public static bool ContainsRequiredAttributes(this IMod mod, IEnumerable<ModdableAttribute> attributes)
            => mod.RequiredAttributes.All(x => attributes.Contains(x));
    }
}