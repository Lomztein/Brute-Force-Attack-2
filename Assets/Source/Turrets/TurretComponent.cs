using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.ExpansionCards;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public abstract class TurretComponent : MonoBehaviour, ITurretComponent, IModdable, IExpansionCardAcceptor, IPurchasable, IGridObject
    {
        public ITurretAssembly Assembly { get; set; }

        [ModelProperty] [SerializeField] protected string _identifier;
        public string UniqueIdentifier => _identifier;

        [ModelProperty] [SerializeField] protected string _name;
        public string Name => _name;
        [ModelProperty] [SerializeField] protected string _description;
        public string Description => _description;
        [ModelProperty] [SerializeField] protected ResourceCost _cost;
        public IResourceCost Cost => _cost;
        public Sprite Sprite => GetComponentInChildren<SpriteRenderer>().sprite;

        [ModelProperty] [SerializeField] protected List<ModdableAttribute> _modAttributes;
        public ModdableAttribute[] Attributes => _modAttributes.ToArray();

        public IStatContainer Stats { get; private set; } = new StatContainer();
        public IEventContainer Events { get; private set; } = new EventContainer();
        public IModContainer Mods { get; private set; }

        [SerializeField][ModelProperty]
        protected Size _width;

        [SerializeField][ModelProperty]
        protected Size _height;
        public Size Width => _width;
        public Size Height => _height;

        [ModelProperty]
        public float PassiveHeatProduction;
        [ModelProperty]
        public StatBaseValues StatBaseValues;
        [ModelProperty]
        public int ExpansionCardCapacity;
        private List<IExpansionCard> _expansionCards = new List<IExpansionCard>();

        public void Start()
        {
            Mods = new ModContainer(Stats, Events);
            InternalInitComponent();
            Init();
        }

        public void OnInstantiated()
        {
            InternalInitComponent();
        }

        private void InternalInitComponent()
        {
            InitStats();
            InitEvents();
            Stats.Init(StatBaseValues);
        }

        public void FixedUpdate()
        {
            if (Assembly.Enabled)
            {
                HeatAssembly(PassiveHeatProduction, Time.fixedDeltaTime);
                Tick(Time.fixedDeltaTime);
            }
        }

        public void OnDestroy()
        {
            End();
        }

        private void HeatAssembly (float amount, float dt)
        {
            if (Assembly != null)
            {
                Assembly.Heat(PassiveHeatProduction * dt);
            }
        }


        public abstract void InitStats();
        public abstract void InitEvents();
        public abstract void Init();
        public abstract void Tick(float deltaTime);
        public abstract void End();

        public bool InsertCard(IExpansionCard card)
        {
            if (HasCapacity ())
            {
                _expansionCards.Add(card);
                return true;
            }
            return false;
        }

        public bool RemoveCard(IExpansionCard card)
        {
            return _expansionCards.Remove(card);
        }

        public override string ToString()
        {
            return Name;
        }

        protected void AddAttribute (ModdableAttribute attribute)
        {
            if (!_modAttributes.Contains (attribute))
            {
                _modAttributes.Add(attribute);
            }
        }

        public bool HasCapacity()
        {
            return _expansionCards.Count <= ExpansionCardCapacity - 1;
        }

        public void Place()
        {
        }

        public void Pickup()
        {
        }
    }
}