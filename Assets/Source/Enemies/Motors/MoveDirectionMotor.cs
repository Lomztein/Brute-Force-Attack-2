using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Motors
{
    public class MoveDirectionMotor : MonoBehaviour, IEnemyMotor
    {
        public Vector3 Direction;
        public float Speed;

        public void Tick(float deltaTime)
        {
            transform.Translate(Direction * Speed * deltaTime);
        }
    }
}
