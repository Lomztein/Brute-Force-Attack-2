using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Targeting
{
    public class TransformTarget : ITarget
    {
        private Transform _target;

        public TransformTarget(Transform target)
        {
            _target = target;
        }

        public Vector3 GetPosition()
            => _target.position;

        public bool IsValid()
            => _target != null;
    }
}
