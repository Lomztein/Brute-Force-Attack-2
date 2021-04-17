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
        public Vector3[] Path {get; set; }
        public int PathIndex { get; set; }

        public float Speed { get; set; }
        [ModelProperty]
        public bool Rotate;

        public bool HasReachedEnd()
        {
            return PathIndex == Path.Length;
        }

        public void SetPath(Vector3[] path)
        {
            Path = path;
        }

        public void Tick(float deltaTime)
        {
            if (PathIndex < Path.Length)
            {
                Vector3 waypoint = Path[PathIndex];
                Vector3 direction = (waypoint - transform.position).normalized;

                transform.position += direction * Speed * deltaTime;

                if (Rotate)
                {
                    transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)), Speed * 60f * deltaTime);
                }

                if ((waypoint - transform.position).sqrMagnitude <= Mathf.Pow(Speed * deltaTime, 2))
                {
                    PathIndex++;
                }
            }
        }
    }
}
