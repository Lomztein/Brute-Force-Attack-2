using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Assemblers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public class TurretAssembly : MonoBehaviour, ITurretAssembly, IGridPlaceable, IPurchasable, IModdable
    {
        private List<ITurretComponent> _components;
        private ITurretAssembler _assembler = new TurretAssembler();

        public IStatContainer Stats = new StatContainer ();
        public IEventContainer Events = new EventContainer();
        public IModContainer Mods { get; private set; }

        private IStatReference _passiveCooling;
        private IStatReference _heatCapacity;

        public bool Enabled { get; private set; } = true;

        [ModelProperty]
        public StatBaseValues StatBaseValues;

        public float Heat;

        [ModelProperty][SerializeField] private string _name;
        public string Name => _name;
        [ModelProperty][SerializeField] private string _description;
        public string Description => _description;
        [ModelProperty][SerializeField] private ResourceCost _cost;
        public IResourceCost Cost => _cost;
        public Sprite Sprite => GetComponentInChildren<SpriteRenderer>().sprite;

        public Size Size => Size.Small;

        // Start is called before the first frame update
        void Awake()
        {
            ResetComponentList();
            InitStats();
        }

        void InitStats ()
        {
            Mods = new ModContainer(Stats, Events);

            _passiveCooling = Stats.AddStat("PassiveCooling", "Passive Cooling", "How quickly the passively cools.");
            _heatCapacity = Stats.AddStat("HeatCapacity", "Heat Capacity", "Total heat capacity before a complete temporary shutdown.");

            Stats.Init(StatBaseValues);
        }

        void ResetComponentList ()
        {
            _components = GetComponentsInChildren<ITurretComponent>().ToList();
            Reassemble();
        }

        public void AddComponent (ITurretComponent component)
        {
            ResetComponentList();
        }

        public void RemoveComponent (ITurretComponent component)
        {
            ResetComponentList();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Heat -= _passiveCooling.GetValue () * Time.fixedDeltaTime;
        }

        private void Reassemble ()
        {
            _assembler.Assemble(this);
        }

        public ITurretComponent[] GetComponents()
        {
            return _components.ToArray();
        }

        void ITurretAssembly.Heat(float amount)
        {
            Heat += amount;
        }

        public void Place()
        {
            Enabled = true;
        }

        public void Pickup()
        {
            Enabled = false;
        }
    }
}