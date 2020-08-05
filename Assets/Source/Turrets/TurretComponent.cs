﻿using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Misc;
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

        int IExpansionCardAcceptor.ExpansionCardCapacity => ExpansionCardCapacity;
        public IExpansionCard[] ExpansionCards => _expansionCards.ToArray();

        [ModelProperty]
        public float PassiveHeatProduction;
        [ModelProperty]
        public StatBaseValues StatBaseValues;
        [ModelProperty]
        public int ExpansionCardCapacity;
        private List<IExpansionCard> _expansionCards = new List<IExpansionCard>();

        public void Start()
        {
            InitComponent();
        }

        public void OnInstantiated()
        {
            InitComponent();
        }

        private void InitComponent()
        {
            Mods = new ModContainer(Stats, Events);

            Init();
            Stats.Init(StatBaseValues);

            SceneAssemblyManager.Instance.AddComponent(this);
        }

        public void FixedUpdate()
        {
            Tick(Time.fixedDeltaTime);
        }

        public void OnDestroy()
        {
            End();
            SceneAssemblyManager.Instance.RemoveComponent(this);
        }

        public abstract void Init();
        public abstract void Tick(float deltaTime);
        public abstract void End();

        public bool InsertExpansionCard(IExpansionCard card)
        {
            if (HasCapacity ())
            {
                _expansionCards.Add(card);
                return true;
            }
            return false;
        }

        public bool RemoveExpansionCard(IExpansionCard card)
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