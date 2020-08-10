using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public abstract class TurretComponent : MonoBehaviour, ITurretComponent, IModdable, IPurchasable, IGridObject
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

        public void Place()
        {
        }

        public void Pickup()
        {
        }

        public bool IsCompatableWith(IMod mod)
            => mod.ContainsRequiredAttributes(_modAttributes);
    }
}