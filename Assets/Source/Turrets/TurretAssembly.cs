using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public class TurretAssembly : MonoBehaviour, ITurretAssembly, IGridObject, IPurchasable, IModdable
    {
        public IStatContainer Stats = new StatContainer ();
        public IEventContainer Events = new EventContainer();
        public IModContainer Mods { get; private set; }

        private IStatReference _passiveCooling;
        private IStatReference _heatCapacity;

        public bool Enabled { get => enabled; private set => enabled = value; }

        [ModelProperty]
        public StatBaseValues StatBaseValues;

        public float Heat;
        [ModelProperty][SerializeField] private string _name;
        public string Name { get => _name; set => _name = value; }
        [ModelProperty][SerializeField] private string _description;
        public string Description { get => _description; set => _description = value; }

        public IResourceCost Cost => GetCost();

        private IResourceCost GetCost()
        {
            IEnumerable<IPurchasable> children = GetComponentsInChildren<ITurretComponent>().Select (x => x as IPurchasable).Where (x => x != null);
            return children.Select(x => x.Cost).Sum();
        }

        public Sprite Sprite => Sprite.Create (Iconography.GenerateIcon (gameObject), new Rect (0f, 0f, Iconography.RENDER_SIZE, Iconography.RENDER_SIZE), Vector2.one / 2f);

        public Size Width => GetRootComponent().Width;
        public Size Height => GetRootComponent().Height;

        [SerializeField] [ModelProperty] ModdableAttribute[] _modAttributes;
        public ModdableAttribute[] Attributes => _modAttributes;

        // Start is called before the first frame update
        void Start()
        {
            AssemblyManager.Instance.AddAssembly(this);
            InitStats();
        }

        void OnDestroy ()
        {
            AssemblyManager.Instance.RemoveAssembly(this);
        }

        void InitStats ()
        {
            Mods = new ModContainer(Stats, Events);

            _passiveCooling = Stats.AddStat("PassiveCooling", "Passive Cooling", "How quickly the passively cools.");
            _heatCapacity = Stats.AddStat("HeatCapacity", "Heat Capacity", "Total heat capacity before a complete temporary shutdown.");

            Stats.Init(StatBaseValues);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Heat -= _passiveCooling.GetValue () * Time.fixedDeltaTime;
        }

        public ITurretComponent[] GetComponents()
        {
            return GetComponentsInChildren<ITurretComponent>();
        }

        void ITurretAssembly.Heat(float amount)
        {
            Heat += amount;
        }

        public override string ToString()
        {
            return Name;
        }

        public ITurretComponent GetRootComponent()
        {
            return GetComponentInChildren<ITurretComponent>();
        }
    }
}