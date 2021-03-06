﻿using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Attachment;
using Lomztein.BFA2.World;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Structures.Turrets
{
    public abstract class TurretComponent : Structure, IModdable, IAttachable
    {
        public override Sprite Sprite => GetComponentInChildren<SpriteRenderer>().sprite;

        public Vector3 WorldPosition => transform.position;
        public Quaternion WorldRotation => transform.rotation;

        [ModelProperty]
        public float BaseComplexity;

        [ModelProperty]
        public AttachmentSlotSet AttachmentSlots;
        [ModelProperty]
        public AttachmentPoint[] AttachmentPoints;
        protected TurretAssembly _assembly;

        protected override void Awake()
        {
            base.Awake();
            InitSelf();
        }

        public void Start()
        {
            InitComponent();
        }

        public void OnInstantiated()
        {
            InitComponent();
        }

        private void InitSelf ()
        {
            _assembly = GetComponentInParent<TurretAssembly>();
            PreInit();
        }

        private void InitComponent()
        {
            Init();

            StartCoroutine(DelayedPostInit());
        }

        private IEnumerator DelayedPostInit ()
        {
            yield return new WaitForEndOfFrame();
            PostInit();
        }

        public void FixedUpdate()
        {
            Tick(Time.fixedDeltaTime);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            End();
        }

        public virtual void PreInit() { }
        public abstract void Init();
        public virtual void PostInit () { }

        public abstract void Tick(float deltaTime);
        public abstract void End();

        public override string ToString()
        {
            return $"{Name}\n\t{Stats}";
        }

        public void OnDrawGizmosSelected()
        {
            foreach (var point in AttachmentSlots.GetPoints().Select(x => x.GetWorldPosition(transform.position, transform.rotation)))
            {
                Gizmos.DrawWireSphere(point, 0.25f);
            }
            foreach (var point in GetPoints().Select(x => x.GetWorldPosition(transform.position, transform.rotation)))
            {
                Gizmos.DrawSphere(point, 0.2f);
            }
        }

        public virtual float ComputeComplexity() => BaseComplexity;

        public IEnumerable<AttachmentPoint> GetPoints() => AttachmentPoints;
    }
}