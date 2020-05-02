using Lomztein.BFA2.Pooling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles
{
    public class ProjectileInfo : IProjectileInfo
    {
        public Vector3 Direction { get; set; }
        public float Speed { get; set; }
        public Vector3 Position { get; set; }
        public float Damage { get; set; }
        public float Range { get; set; }
        public int Layer { get; set; }
        public Color Color { get; set; }
        public Transform Target { get; set; }
        public IObjectPool<IProjectile> Pool { get; set; }

        public IProjectileInfo Clone()
        {
            ProjectileInfo info = new ProjectileInfo();

            info.Direction = Direction;
            info.Speed = Speed;
            info.Position = Position;
            info.Damage = Damage;
            info.Range = Range;
            info.Layer = Layer;
            info.Color = Color;
            info.Target = Target;
            info.Pool = Pool;

            return info;
        }
    }
}
