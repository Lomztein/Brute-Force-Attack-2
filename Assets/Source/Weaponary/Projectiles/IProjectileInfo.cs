using Lomztein.BFA2.Pooling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles
{
    public interface IProjectileInfo
    {
        Vector3 Direction { get; set; }
        float Speed { get; set; }
        Vector3 Position { get; set; }
        float Damage { get; set; }
        float Range { get; set; }
        int Layer { get; set; }
        Color Color { get; set; }
        Transform Target { get; set; }
        IObjectPool<IProjectile> Pool { get; set; }

        IProjectileInfo Clone();
    }
}
