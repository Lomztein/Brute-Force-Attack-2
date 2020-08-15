using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Motors
{
    public class FollowPathMotor : MonoBehaviour, IEnemyMotor
    {
        private Vector3[] _path;
        private int _pathIndex;

        public float Speed { get; set; }
        [ModelProperty]
        public bool Rotate;

        public bool HasReachedEnd()
        {
            return _pathIndex == _path.Length;
        }

        public void SetPath(Vector3[] path)
        {
            _path = path;
        }

        public void Tick(float deltaTime)
        {
            if (_pathIndex < _path.Length)
            {
                Vector3 waypoint = _path[_pathIndex];
                Vector3 direction = (waypoint - transform.position).normalized;

                transform.position += direction * Speed * deltaTime;

                if (Rotate)
                {
                    transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)), Speed * 60f * deltaTime);
                }

                if ((waypoint - transform.position).sqrMagnitude <= Mathf.Pow(Speed * deltaTime, 2))
                {
                    _pathIndex++;
                }
            }
        }
    }
}
