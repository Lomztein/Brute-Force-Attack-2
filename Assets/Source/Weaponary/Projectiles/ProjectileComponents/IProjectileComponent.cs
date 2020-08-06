using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents
{
    interface IProjectileComponent
    {
        void Init(IProjectile parent);

        void Tick(float deltaTime);

        void End();
    }
}
