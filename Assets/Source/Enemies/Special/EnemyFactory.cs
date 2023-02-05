using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Special
{
    public class EnemyFactory : MonoBehaviour
    {
        [ModelProperty]
        public ContentPrefabReference ToSpawn;
        [ModelProperty]
        public Vector2[] SpawnPositions;
        [ModelProperty]
        public float SpawnDelay;

        private Enemy _enemy;

        // Start is called before the first frame update
        void Start()
        {
            _enemy = GetComponent<Enemy>();
            InvokeRepeating(nameof(Spawn), SpawnDelay, SpawnDelay);
        }

        private void Spawn ()
        {
            foreach (Vector2 pos in SpawnPositions)
            {
                Vector3 wpos = transform.position + (Vector3)pos;
                GameObject newEnemyGO = ToSpawn.Instantiate();
                Enemy enemy = newEnemyGO.GetComponent<Enemy>();
                enemy.Init(wpos, _enemy.Path, _enemy.WaveHandler);
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
