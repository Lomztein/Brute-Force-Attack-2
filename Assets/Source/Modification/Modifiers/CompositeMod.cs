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
    public class CompositeMod : MonoBehaviour, IMod
    {
        private static string _childName = "Modifiers";
        private IMod[] _internalMods;

        public ModdableAttribute[] RequiredAttributes => _internalMods.SelectMany(x => x.RequiredAttributes).ToArray();

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

        private void Awake()
        {
            _internalMods = transform.Find(_childName).GetComponents<IMod>();
        }

        public void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in _internalMods)
            {
                mod.ApplyBase(stats, events);
            }
        }

        public void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in _internalMods)
            {
                mod.ApplyStack(stats, events);
            }
        }

        public void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in _internalMods)
            {
                mod.RemoveBase(stats, events);
            }
        }

        public void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            foreach (IMod mod in _internalMods)
            {
                mod.RemoveStack(stats, events);
            }
        }
    }
}
