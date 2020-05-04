using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public abstract class TurretComponent : MonoBehaviour, ITurretComponent
    {
        public ITurretAssembly Assembly { get; set; }

        [ModelProperty]
        public float PassiveHeatProduction;

        public void Start()
        {
            Init();
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

        public abstract void Tick(float deltaTime);
        public abstract void Init();
        public abstract void End();
    }
}