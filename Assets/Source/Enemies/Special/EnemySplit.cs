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
        private IEnemy _enemy;

        private void Start()
        {
            _enemy = GetComponent<IEnemy>();
            _enemy.OnKilled += Enemy_OnKilled;
        }

        private void Enemy_OnKilled(IEnemy obj)
        {
            foreach (Vector2 pos in SpawnPositions)
            {
                Vector3 wpos = transform.position + (Vector3)pos;
                GameObject newEnemyGO = ToSpawn.Instantiate();
                IEnemy enemy = newEnemyGO.GetComponent<IEnemy>();
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
