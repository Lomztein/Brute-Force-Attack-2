using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Globals.Managers
{
    public class ComponentGlobalModManager : GlobalModManagerBase<TurretComponent>
    {
        private List<GlobalMod> _currentMods = new List<GlobalMod>();

        private void Start()
        {
            SceneAssemblyManager.Instance.OnComponentAdded += OnComponentAdded;
        }

        private void OnComponentAdded(ITurretComponent obj)
        {
            if (obj is TurretComponent concreteComponent)
            {
                foreach (var mod in _currentMods)
                {
                    if (ShouldApplyTo(mod, concreteComponent))
                    {
                        ApplyTo(mod, concreteComponent);
                    }
                }
            }
        }

        public override void RemoveMod(GlobalMod mod)
        {
            _currentMods.Remove(mod);

            foreach (ITurretComponent component in SceneAssemblyManager.Instance.Components)
            {
                if (component is TurretComponent concreteComponent)
                {
                    if (ShouldApplyTo(mod, concreteComponent))
                    {
                        RemoveFrom(mod, concreteComponent);
                    }
                }
            }
        }

        public override void TakeMod(GlobalMod mod)
        {
            _currentMods.Add(mod);

            foreach (ITurretComponent component in SceneAssemblyManager.Instance.Components)
            {
                if (component is TurretComponent concreteComponent)
                {
                    if (ShouldApplyTo(mod, concreteComponent))
                    {
                        ApplyTo(mod, concreteComponent);
                    }
                }
            }
        }

        private bool ShouldApplyTo(GlobalMod mod, TurretComponent component)
            => mod.Filter(component);

        private void RemoveFrom(GlobalMod mod, TurretComponent component)
        {
            component.Mods.RemoveMod(mod.Mod.Identifier);
        }

        private void ApplyTo (GlobalMod mod, TurretComponent component)
        {
            component.Mods.AddMod(mod.Mod);
        }
    }
}
