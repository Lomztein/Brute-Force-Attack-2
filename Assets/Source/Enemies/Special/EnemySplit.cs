using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Special
{
    public class EnemySplit : MonoBehaviour
    {
        [ModelProperty]
        public ContentPrefabReference ToSpawn;
        [ModelProperty]
        public Vector2[] SpawnPositions;
        private Enemy _enemy;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            _enemy.OnKilled += Enemy_OnKilled;
        }

        private void Enemy_OnKilled(Enemy obj)
        {
            foreach (Vector2 pos in SpawnPositions)
            {
                Vector3 wpos = transform.position + (Vector3)pos;
                GameObject newEnemyGO = ToSpawn.Instantiate();
                Enemy enemy = newEnemyGO.GetComponent<Enemy>();
                enemy.Init(wpos, obj.Path, obj.WaveHandler);
                enemy.PathIndex = _enemy.PathIndex;
                enemy.WaveHandler.AddEnemy(enemy);
            }
        }

        private void OnDrawGizmosSelected()
        {
            foreach (Vector2 position in SpawnPositions)
            {
                Gizmos.DrawSphere(transform.position + (Vector3)position, 0.25f);
            }
        }
    }
}
