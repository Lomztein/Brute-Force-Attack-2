using Lomztein.BFA2.Misc;
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

namespace Lomztein.BFA2.Structures.Turrets
{
    public abstract class TurretComponent : Structure, INamed, IModdable, IPurchasable, IGridObject
    {
        public override Sprite Sprite => GetComponentInChildren<SpriteRenderer>().sprite;

        protected IAttachmentPointSet _upperAttachmentPoints;
        protected IAttachmentPointSet _lowerAttachmentPoints;

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
            _upperAttachmentPoints = new SquareAttachmentPointSet(Width, Height);
            _lowerAttachmentPoints = new SquareAttachmentPointSet(Width, Height);

            AttachToParent();

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
            DetachAttachmentPoints();
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

        private void AttachToParent ()
        {
            if (transform.parent != null)
            {
                TurretComponent parent = transform.parent.GetComponent<TurretComponent>();
                if (parent != null)
                {
                    foreach (AttachmentPoint point in GetLowerAttachmentPoints())
                    {
                        AttachmentPoint pPoint = parent.GetUpperAttachmentPoints().GetPoint(transform.parent.position, point.LocalToWorldPosition(transform.position));
                        if (pPoint != null)
                        {
                            point.AttachTo(pPoint);
                            pPoint.AttachTo(point);
                        }
                    }
                }
            }
        }

        private void DetachAttachmentPoints ()
        {
            foreach (AttachmentPoint point in GetLowerAttachmentPoints())
            {
                point.AttachedPoint?.Detach();
                point.Detach();
            }

            foreach (AttachmentPoint point in GetUpperAttachmentPoints())
            {
                point.AttachedPoint?.Detach();
                point.Detach();
            }
        }

        public AttachmentPoint[] GetUpperAttachmentPoints()
            => _upperAttachmentPoints?.GetPoints() ?? new AttachmentPoint[0];

        public AttachmentPoint[] GetLowerAttachmentPoints()
            => _lowerAttachmentPoints?.GetPoints() ?? new AttachmentPoint[0];

        public void OnDrawGizmosSelected()
        {
            foreach (var point in GetUpperAttachmentPoints().LocalToWorldPosition(transform.position))
            {
                Gizmos.DrawWireSphere(point, 0.25f);
            }
            foreach (var point in GetLowerAttachmentPoints().LocalToWorldPosition(transform.position))
            {
                Gizmos.DrawSphere(point, 0.2f);
            }
        }


    }
}