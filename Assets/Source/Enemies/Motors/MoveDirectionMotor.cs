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

        public float Range;
        private bool _finished;

        private void Awake()
        {
            Invoke("Finish", Range / Speed);
        }

        public bool HasReachedEnded()
        {
            return _finished;
        }

        public void Tick(float deltaTime)
        {
            transform.Translate(Direction * Speed * deltaTime);
        }

        private void Finish ()
        {
            _finished = true;
        }
    }
}
