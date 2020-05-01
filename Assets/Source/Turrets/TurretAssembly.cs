using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Assemblers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public class TurretAssembly : MonoBehaviour, ITurretAssembly
    {
        private List<ITurretComponent> _components;
        private ITurretAssembler _assembler = new TurretAssembler();

        private IStatContainer _statContainer = new StatContainer ();

        private IStatReference _passiveCooling;
        private IStatReference _heatCapacity;

        [ModelProperty]
        public StatBaseValues StatBaseValues;

        public float Heat;

        // Start is called before the first frame update
        void Start()
        {
            ResetComponentList();
            InitStats();
        }

        void InitStats ()
        {
            _passiveCooling = _statContainer.AddStat("PassiveCooling", "Passive Cooling", "");
            _heatCapacity = _statContainer.AddStat("HeatCapacity", "Heat Capacity", "");

            _statContainer.Init(StatBaseValues);
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
    }
}