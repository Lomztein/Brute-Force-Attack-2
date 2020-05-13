using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Pooling;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles
{
    public class ProjectileInstantiator : MonoBehaviour, IProjectileInstantiator
    {
        [ModelProperty]
        public ContentGameObject ContentPrefabObject;
        private GameObjectPrefab _prefab;

        public IObjectPool<IProjectile> Source { get; set; }

        private void Awake()
        {
            _prefab = ContentPrefabObject.GetPrefab();
            Source = new NoGameObjectPool<IProjectile>(_prefab);
        }

        private void OnDestroy()
        {
            if (_prefab != null)
            {
                _prefab.Dispose();
            }
        }

        public IProjectile[] Create(IProjectileInfo info, Vector3 position, Quaternion rotation, int amount, float deviation, float speed) 
        {
            IProjectile[] projectiles = new IProjectile[amount];
            for (int i = 0; i < amount; i++)
            {
                IProjectile projectile = Source.Get();
                projectile.Info = info.Clone();

                float d = UnityEngine.Random.Range(-deviation, deviation);
                Vector3 direction = rotation * new Vector3(1f, Mathf.Sin(Mathf.Deg2Rad * d), 0f);

                projectile.Info.Direction = direction;
                projectile.Info.Speed = speed;
                projectile.Info.Pool = Source;
                projectile.Info.Position = position;

                projectile.Init();
            }
            return projectiles;
        }
    }
}
