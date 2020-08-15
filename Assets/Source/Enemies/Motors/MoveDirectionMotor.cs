using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.EngineObjectSerializers;
using Newtonsoft.Json.Linq;
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
        public float Speed { get; set; }
        [ModelProperty]
        public float Range;

        private bool _finished;

        private void Start()
        {
            Invoke("Finish", Range / Speed);
        }

        public bool HasReachedEnd()
        {
            return _finished;
        }

        public void Tick(float deltaTime)
        {
            transform.Translate(Vector3.down * Speed * deltaTime);
        }

        private void Finish ()
        {
            _finished = true;
        }

        public void SetPath(Vector3[] path)
        {
        }
    }
}
