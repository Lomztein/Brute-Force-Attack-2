using Lomztein.BFA2.Structures.Turrets.Weapons;
using Lomztein.BFA2.Weaponary;
using Lomztein.BFA2.Weaponary.FireSynchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Structures.Turrets.Misc
{
    public class WeaponFireSynchronizer : TurretComponent
    {
        public SequencedFireSyncronizationController Controller { get; private set; }

        public override void End()
        {
        }

        public override void Init()
        {
            SynchronizeChildren();
        }

        public override void Tick(float deltaTime)
        {
        }

        public void OnStructureChanged ()
        {
            SynchronizeChildren();
        }

        private void SynchronizeChildren ()
        {
            var groups = GetChildWeapons().GroupBy(x => x.Identifier);
            if (groups.Count() == 1)
            {
                var group = groups.First();
                TurretWeapon first = group.First();

                Controller = new SequencedFireSyncronizationController(1 / first.Firerate.GetValue());

                foreach (var weapon in group)
                {
                    SequencedFireSynchronization sync = new SequencedFireSynchronization(Controller);
                    Controller.AddSync(sync);
                    (weapon.Weapon as Weapon).Synchronize(sync);
                }
            }
        }

        private IEnumerable<TurretWeapon> GetChildWeapons ()
            => GetComponentsInChildren<TurretWeapon>();
    }
}
