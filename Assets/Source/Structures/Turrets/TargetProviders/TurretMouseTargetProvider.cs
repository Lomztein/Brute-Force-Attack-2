﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Events.EventArgs;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.TargetProviders
{
    public class TurretMouseTargetProvider : TurretComponent, ITargetProvider
    {
        private Transform _mousePointer;

        public IEventCaller<TargetEventArgs> OnTargetAcquired { get; private set; }

        public override StructureCategory Category => StructureCategories.TargetFinder;

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
            OnTargetAcquired = Events.AddEvent<TargetEventArgs>("OnTargetAcquired", "On Target Acquired", "Fires when the mouse pointer is initialized.");
            _mousePointer = new GameObject("MousePointer").transform;
        }

        public override void Tick(float deltaTime)
        {
            _mousePointer.position = Input.WorldMousePosition;
        }
    }
}
