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

        [ModelProperty]
        public float PassiveCooling;
        [ModelProperty]
        public float HeatCapacity;

        public float Heat { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            ResetComponentList();
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
            Heat -= PassiveCooling * Time.fixedDeltaTime;
        }

        private void Reassemble ()
        {
            _assembler.Assemble(this);
        }

        public ITurretComponent[] GetComponents()
        {
            return _components.ToArray();
        }
    }
}