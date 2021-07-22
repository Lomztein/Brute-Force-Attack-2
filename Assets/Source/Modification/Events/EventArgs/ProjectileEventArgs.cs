using Lomztein.BFA2.Weaponary.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Events.EventArgs
{
    public class ProjectileEventArgs : IEventArgs
    {
        public IProjectile Projectile { get; private set; }

        public ProjectileEventArgs(IProjectile projectile)
        {
            Projectile = projectile;
        }
    }
}
