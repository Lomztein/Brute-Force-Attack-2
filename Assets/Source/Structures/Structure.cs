using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Turrets;
using Lomztein.BFA2.UI;
using Lomztein.BFA2.UI.ToolTip;
using Lomztein.BFA2.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Structures
{
    public class Structure : MonoBehaviour, INamed, IPurchasable, IGridObject, IIdentifiable, IModdable, ITagged
    {
        [SerializeField]
        [ModelProperty]
        public string _name;
        public virtual string Name { get => _name; set => _name = value; }

        [SerializeField]
        [ModelProperty]
        [TextArea]
        public string _description;

        [SerializeField]
        [ModelProperty]
        public string _uniqueIdentifier;
        public virtual string Identifier => _uniqueIdentifier;

        public virtual string Description { get => _description; set => _description = value; }
        public virtual IResourceCost Cost => _cost;
        [SerializeField]
        [ModelProperty]
        public ResourceCost _cost;

        public IStatContainer Stats = new StatContainer();
        public IEventContainer Events = new EventContainer();
        public IModContainer Mods { get; private set; }

        private bool _initialized;

        [ModelProperty]
        public TagSet Tags = new TagSet();

        public virtual Sprite Sprite => Iconography.GenerateSprite(gameObject);

        [SerializeField]
        [ModelProperty]
        public Size _width;
        public virtual Size Width => _width;
        [SerializeField]
        [ModelProperty]
        public Size _height;
        public virtual Size Height => _height;
        public bool Flipped => transform.localScale.y < 0f;
        
        [ModelAssetReference]
        public StructureCategory Category;

        public event Action<Structure, IStatReference, object> StatChanged;
        public event Action<Structure, IEventReference, object> EventChanged;
        public event Action<Structure, GameObject, object> HierarchyChanged;
        public event Action<Structure> Destroyed;

        public override string ToString()
        {
            return Name + "\n\n" + Stats.ToString();
        }

        private void InternalInit ()
        {
            if (!_initialized)
            {
                Mods = new ModContainer(this, Stats, Events);

                Stats.OnStatChanged += Stats_OnStatChanged;
                Stats.OnStatAdded += Stats_OnStatChanged;
                Stats.OnStatRemoved += Stats_OnStatChanged;

                Events.OnEventChanged += Events_OnEventChanged;
                Events.OnEventAdded += Events_OnEventChanged;

                AwakeInit();

                _initialized = true;
            }
        }

        private void Awake() => InternalInit();
        public void OnInstantiated() => InternalInit();
        public void OnAssembled() => InternalInit();

        protected virtual void AwakeInit() { }


        private void Events_OnEventChanged(IEventReference arg1, object arg2)
        {
            InvokeEventChanged(arg1, arg2);
        }

        private void Stats_OnStatChanged(IStatReference arg1, object arg2)
        {
            InvokeStatChanged(arg1, arg2);
        }

        public void Flip ()
        {
            if (Flipped)
            {
                transform.localScale = Vector3.one;
            }
            else
            {
                transform.localScale = new Vector3(1f, -1f, 1f);
            }
        }

        public void InvokeHierarchyChanged(GameObject changedObj, object source)
        {
            BroadcastMessage("OnHierarchyChanged", SendMessageOptions.DontRequireReceiver);
            HierarchyChanged?.Invoke(this, changedObj, source);
        }

        public void InvokeStatChanged (IStatReference stat, object source)
        {
            BroadcastMessage("OnStatChanged", SendMessageOptions.DontRequireReceiver);
            StatChanged?.Invoke(this, stat, source);
        }

        public void InvokeEventChanged (IEventReference eventRef, object source)
        {
            BroadcastMessage("OnEventChanged", SendMessageOptions.DontRequireReceiver);
            EventChanged?.Invoke(this, eventRef, source);
        }

        protected virtual void OnDestroy ()
        {
            BroadcastMessage("OnStructureDestroyed", SendMessageOptions.DontRequireReceiver);
            Destroyed?.Invoke(this);
        }

        public bool AddTag (string tag)
        {
            return Tags.AddTag(tag);
        }

        public string[] GetTags()
        {
            return Tags.ToArray();
        }

        public bool OverlapsCircle (Vector3 position, float range)
        {
            float margin = 0.45f;
            range += margin;
            foreach (Vector2 point in World.Grid.GenerateGridPoints(transform.position, Width, Height))
            {
                float sqrDist = (position - (Vector3)point).sqrMagnitude;
                if (sqrDist < range * range)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasTag(string tag)
        {
            return Tags.HasTag(tag);
        }
    }
}