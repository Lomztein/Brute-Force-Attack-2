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
    public abstract class Mod : ScriptableObject
    {
        [ModelProperty, SerializeField]
        public string Identifier;
        [ModelProperty, SerializeField]
        public string Name;
        [ModelProperty, SerializeField]
        public string Description;
        [ModelProperty]
        public ModdableAttribute[] RequiredAttributes;

        public bool CanMod(IModdable moddable)
        {
            var attributes = moddable.GetModdableAttributes();
            return RequiredAttributes.All(x => attributes.Contains(x));
        }

        public abstract void ApplyBase(IStatContainer stats, IEventContainer events);
        public abstract void ApplyStack(IStatContainer stats, IEventContainer events);
        public abstract void RemoveBase(IStatContainer stats, IEventContainer events);
        public abstract void RemoveStack(IStatContainer stats, IEventContainer events);
    }
}
