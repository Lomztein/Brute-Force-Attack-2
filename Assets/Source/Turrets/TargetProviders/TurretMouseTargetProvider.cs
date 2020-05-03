using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.TargetProviders
{
    public class TurretMouseTargetProvider : TurretComponent, ITargetProvider
    {
        private Transform _mousePointer;

        public override void End()
        {
            if (_mousePointer)
            {
                Destroy(_mousePointer.gameObject);
            }
        }

        public Transform GetTarget()
        {
            return _mousePointer;
        }

        public override void Init()
        {
            _mousePointer = new GameObject("MousePointer").transform;
        }

        public override void Tick(float deltaTime)
        {
            _mousePointer.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
