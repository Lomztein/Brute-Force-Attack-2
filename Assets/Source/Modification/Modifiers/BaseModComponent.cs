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
        [ModelProperty] [SerializeField] protected string _identifier;
        public string Identifier => _identifier;

        [ModelProperty] [SerializeField] protected string _name;
        public string Name => _name;

        [ModelProperty] [SerializeField] protected string _description;
        public string Description => _description;

        [ModelProperty]
        [SerializeField]
        protected ModdableAttribute[] _requiredAttributes;
        public ModdableAttribute[] RequiredAttributes => _requiredAttributes;

        public abstract void ApplyBase(IStatContainer stats, IEventContainer events);
        public abstract void ApplyStack(IStatContainer stats, IEventContainer events);
        public abstract void RemoveBase(IStatContainer stats, IEventContainer events);
        public abstract void RemoveStack(IStatContainer stats, IEventContainer events);
    }
}
