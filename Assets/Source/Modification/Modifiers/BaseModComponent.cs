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
    public abstract class BaseModComponent : MonoBehaviour, IMod
    {
        public ModdableAttribute[] RequiredAttributes;

        [ModelProperty] [SerializeField] private string _identifier;
        public string Identifier => _identifier;

        [ModelProperty] [SerializeField] private string _name;
        public string Name => _name;

        [ModelProperty] [SerializeField] private string _description;
        public string Description => _description;

        public abstract void ApplyBase(IStatContainer stats, IEventContainer events);
        public abstract void ApplyStack(IStatContainer stats, IEventContainer events);
        public abstract void RemoveBase(IStatContainer stats, IEventContainer events);
        public abstract void RemoveStack(IStatContainer stats, IEventContainer events);
        public virtual bool CompatableWith(ModdableAttribute[] attributes)
        {
            return RequiredAttributes.All(x => attributes.Contains(x));
        }
    }
}
