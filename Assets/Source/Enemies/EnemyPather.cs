using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies
{
    public class EnemyPather : MonoBehaviour
    {
        private EnemySpawnPoint[] _spawnPoints;
        private EnemyPoint[] _endPoints;

        private void Start()
        {
            CachePoints();
        }

        private void CachePoints()
        {
            _spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint").Select(x => x.GetComponent<EnemySpawnPoint>()).ToArray();
            _endPoints = GameObject.FindGameObjectsWithTag("EnemyEndPoint").Select(x => x.GetComponent<EnemyPoint>()).ToArray();
        }

        public IEnumerator ComputePaths ()
        {
            foreach (EnemySpawnPoint point in _spawnPoints)
            {
                point.ComputePath(_endPoints);
                yield return new WaitForFixedUpdate();
            }
        }

        public bool AnyPathsAvailable() => _spawnPoints.Any(x => !x.PathBlocked);

        public EnemySpawnPoint GetRandomSpawnPoint()
        {
            EnemySpawnPoint[] available = _spawnPoints.Where(x => !x.PathBlocked).ToArray();
            return available[UnityEngine.Random.Range(0, available.Length)];
        }

        private void OnDrawGizmos()
        {
            if (_spawnPoints != null)
            {
                foreach (EnemySpawnPoint point in _spawnPoints)
                {
                    if (!point.PathBlocked)
                    {
                        Vector3[] path = point.GetPath();
                        for (int i = 0; i < path.Length - 1; i++)
                        {
                            Gizmos.DrawLine(path[i], path[i + 1]);
                        }
                    }
                }
            }
        }
    }
}
