using Lomztein.BFA2.World.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies
{
    public class EnemySpawnPoint : EnemyPoint
    {
        private Vector3[] _path;

        public bool PathBlocked => _path == null;
        public Vector3[] GetPath() => _path;

        public void ComputePath (EnemyPoint[] endPoints)
        {
            _path = Pathfinder.Search(transform.position, endPoints.Select(x => x.transform.position).ToArray());
        }
    }
}
