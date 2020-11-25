using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Turrets;
using Lomztein.BFA2.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Structures
{
    public class Structure : MonoBehaviour, IPurchasable, IGridObject, IIdentifiable, IModdable
    {
        [SerializeField]
        [ModelProperty]
        public string _name;
        public virtual string Name { get => _name; set => _name = value; }

        [SerializeField]
        [ModelProperty]
        public string _description;

        [SerializeField]
        [ModelProperty]
        public string _uniqueIdentifier;
        public virtual string UniqueIdentifier => _uniqueIdentifier;

        public virtual string Description { get => _description; set => _description = value; }
        public virtual IResourceCost Cost => _cost;
        [SerializeField]
        [ModelProperty]
        public ResourceCost _cost;

        public IStatContainer Stats = new StatContainer();
        public IEventContainer Events = new EventContainer();
        public IModContainer Mods { get; private set; }

        [ModelProperty] public ModdableAttribute[] BaseAttributes;
        protected List<ModdableAttribute> _modAttributes = new List<ModdableAttribute>();

        public virtual Sprite Sprite => Iconography.GenerateSprite(gameObject);

        [SerializeField]
        [ModelProperty]
        public Size _width;
        public virtual Size Width => _width;
        [SerializeField]
        [ModelProperty]
        public Size _height;
        public virtual Size Height => _height;
        public virtual StructureCategory Category { get; } = StructureCategories.Misc;

        public event Action<Structure> Changed;
        public event Action<Structure> Destroyed;

        public override string ToString()
        {
            return Name;
        }

        protected virtual void Awake()
        {
            Mods = new ModContainer(Stats, Events);
        }

        public void InvokeChanged() => Changed?.Invoke(this);

        protected virtual void OnDestroy ()
        {
            Destroyed?.Invoke(this);
        }

        public void OnAssembled() => Awake();

        public void AddModdableAttribute (ModdableAttribute attribute)
        {
            _modAttributes.Add(attribute);
        }

        public ModdableAttribute[] GetModdableAttributes()
        {
            return Enumerable.Concat(BaseAttributes, _modAttributes).ToArray();
        }
    }
}