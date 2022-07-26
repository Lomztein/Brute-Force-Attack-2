using Lomztein.BFA2.Structures.Turrets.Weapons;
using Lomztein.BFA2.Weaponary;
using Lomztein.BFA2.Weaponary.FireSynchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Misc
{
    public class TurretWeaponFireSynchronizer : TurretComponent
    {
        public SequencedFireControlSynchronizer Controller { get; private set; }
        private List<TurretWeapon> _children = new List<TurretWeapon>();

        public override void End()
        {
        }

        public override void Init()
        {
        }

        public override void PostInit()
        {
            base.PostInit();
            UpdateChildSubscriptions();
            SynchronizeChildren();
        }

        public override void Tick(float deltaTime)
        {
        }

        public void OnHierarchyChanged ()
        {
            UpdateChildSubscriptions();
            SynchronizeChildren();
        }

        private void UpdateChildSubscriptions ()
        {
            foreach (var component in _children)
            {
                component.StatChanged -= Child_StatChanged;
            }
            _children.Clear();

            foreach (var child in GetChildWeapons())
            {
                child.StatChanged += Child_StatChanged;
                _children.Add(child);
            }

        }

        private void Child_StatChanged(Structure arg1, Modification.Stats.IStatReference arg2, object arg3)
        {
            SynchronizeChildren();
        }

        private void SynchronizeChildren ()
        {
            if (Initialized)
            {
                var groups = GetChildWeapons().GroupBy(x => x.Identifier);
                if (groups.Count() == 1)
                {
                    var group = groups.First();
                    TurretWeapon first = group.First();

                    var oldController = Controller;
                    Controller = new SequencedFireControlSynchronizer(1 / first.Firerate.GetValue());

                    foreach (var weapon in group)
                    {
                        // Remove fire control that was part of previous syncronizer.
                        // Yes, this is horribly ineffecient, but it works just fine.
                        if (oldController != null)
                        {
                            (weapon.Weapon as Weapon).RemoveFireControl(x => oldController.Syncs.Contains(x));
                        }
                        SequencedFireControl sync = new SequencedFireControl(Controller);
                        Controller.AddSync(sync);
                        (weapon.Weapon as Weapon).AddFireControl(sync);
                    }
                }
            }
        }

        private IEnumerable<TurretWeapon> GetChildWeapons ()
            => GetComponentsInChildren<TurretWeapon>();
    }
}
