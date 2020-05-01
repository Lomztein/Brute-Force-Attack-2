using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Targeters
{
    public class TurretRotator : TurretComponent, ITargeter
    {
        [ModelProperty]
        public float Speed;

        public float GetDistance()
        {
            throw new System.NotImplementedException();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}