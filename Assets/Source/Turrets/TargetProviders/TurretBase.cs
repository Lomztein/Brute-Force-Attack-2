using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Rangers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.TargetProviders
{
    public class TurretBase : TurretComponent, ITargetProvider, IRanger
    {
        [ModelProperty]
        public int SimultaniousTargets;
        [ModelProperty]
        public float Range;

        public override void End()
        {
        }

        public float GetRange()
        {
            return Range;
        }

        public Transform[] GetTargets()
        {
            return Array.Empty<Transform>();
        }

        public override void Init()
        {
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}