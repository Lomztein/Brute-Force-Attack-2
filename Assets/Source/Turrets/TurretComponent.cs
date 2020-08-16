using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Attachment;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public abstract class TurretComponent : MonoBehaviour, ITurretComponent, INamed, IModdable, IPurchasable, IGridObject
    {
        [ModelProperty] [SerializeField] protected string _identifier;
        public string UniqueIdentifier => _identifier;

        [ModelProperty] [SerializeField] protected string _name;
        public string Name => _name;
        [ModelProperty] [SerializeField] protected string _description;
        public string Description => _description;
        [ModelProperty] [SerializeField] protected ResourceCost _cost;
        public IResourceCost Cost => _cost;
        public Sprite Sprite => GetComponentInChildren<SpriteRenderer>().sprite;

        [ModelProperty] [SerializeField] protected List<ModdableAttribute> _modAttributes;

        public IStatContainer Stats { get; private set; } = new StatContainer();
        public IEventContainer Events { get; private set; } = new EventContainer();
        public IModContainer Mods { get; private set; }

        [SerializeField][ModelProperty]
        protected Size _width;

        [SerializeField][ModelProperty]
        protected Size _height;
        public Size Width => _width;
        public Size Height => _height;

        public abstract TurretComponentCategory Category { get; }

        protected IAttachmentPointSet _upperAttachmentPoints;
        protected IAttachmentPointSet _lowerAttachmentPoints;

        private void Awake()
        {
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
            Mods = new ModContainer(Stats, Events);
            PreInit();
        }

        private void InitComponent()
        {
            _upperAttachmentPoints = new SquareAttachmentPointSet(Width, Height);
            _lowerAttachmentPoints = new SquareAttachmentPointSet(Width, Height);

            AttachToParent();

            Init();

            SceneAssemblyManager.Instance?.AddComponent(this);
            GlobalUpdate.BroadcastUpdate(new ModdableAddedMessage(this));

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

        public void OnDestroy()
        {
            End();
            DetachAttachmentPoints();
            SceneAssemblyManager.Instance?.RemoveComponent(this);
        }

        public virtual void PreInit() { }
        public abstract void Init();
        public virtual void PostInit () { }

        public abstract void Tick(float deltaTime);
        public abstract void End();

        public override string ToString()
        {
            return Name;
        }

        protected void AddAttribute (ModdableAttribute attribute)
        {
            if (!_modAttributes.Contains (attribute))
            {
                _modAttributes.Add(attribute);
            }
        }
        
        private void AttachToParent ()
        {
            if (transform.parent != null)
            {
                ITurretComponent parent = transform.parent.GetComponent<ITurretComponent>();
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

        public bool IsCompatableWith(IMod mod)
            => mod.ContainsRequiredAttributes(_modAttributes);

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