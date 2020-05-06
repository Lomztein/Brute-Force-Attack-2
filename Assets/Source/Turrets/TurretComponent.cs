using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public abstract class TurretComponent : MonoBehaviour, ITurretComponent, IModdable
    {
        public ITurretAssembly Assembly { get; set; }

        public IStatContainer Stats { get; private set; } = new StatContainer();
        public IEventContainer Events { get; private set; } = new EventContainer();
        public IModContainer Mods { get; private set; }

        [ModelProperty]
        public float PassiveHeatProduction;
        [ModelProperty]
        public StatBaseValues StatBaseValues;

        public void Start()
        {
            Mods = new ModContainer(Stats, Events);
            Init();
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

        public abstract void Tick(float deltaTime);
        public abstract void Init();
        public abstract void End();
    }
}