using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.StructureManagement;
using Lomztein.BFA2.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public abstract class ModBroadcaster : MonoBehaviour
    {
        [ModelAssetReference]
        public Mod Mod;
        private List<IModdable> _currentBroadcastTargets = new List<IModdable>();

        protected virtual bool BroadcastOnStart => false;

        public void BroadcastMod()
        {
            var newTargets = GetPotentialBroadcastTargets();
            var missed = new List<IModdable>(_currentBroadcastTargets);

            foreach (var target in newTargets)
            {
                if (!_currentBroadcastTargets.Contains(target))
                {
                    AddMod(target);
                    _currentBroadcastTargets.Add(target);
                }
                missed.Remove(target);
            }

            foreach (var remaining in missed)
            {
                RemoveMod(remaining);
            }
        }

        protected virtual void Start()
        {
            StructureManager.OnStructureAdded += OnStructureChange;
            StructureManager.OnStructureHierarchyChanged += StructureManager_OnStructureHierarchyChanged;
            StructureManager.OnStructureRemoved += OnStructureChange;

            if (BroadcastOnStart)
            {
                DelayedBroadcast();
            }
        }

        private void StructureManager_OnStructureHierarchyChanged(Structure arg1, GameObject arg2, object arg3)
        {
            OnStructureChange(arg1);
        }

        protected virtual void OnDestroy ()
        {
            StructureManager.OnStructureAdded -= OnStructureChange;
            StructureManager.OnStructureHierarchyChanged -= StructureManager_OnStructureHierarchyChanged;
            StructureManager.OnStructureRemoved -= OnStructureChange;
            ClearMod();
        }

        private void OnStructureChange(Structure obj)
        {
            BroadcastMod();
        }

        protected void ClearMod()
        {
            foreach (var target in _currentBroadcastTargets)
            {
                RemoveMod(target);
            }
            _currentBroadcastTargets.Clear();
        }

        private bool AddMod(IModdable moddable)
        {
            if (Mod.CanMod(moddable))
            {
                moddable.Mods.AddMod(Instantiate(Mod));
                return true;
            }
            return false;
        }

        private void RemoveMod (IModdable moddable)
        {
            if (moddable != null)
            {
                moddable.Mods.RemoveMod(Mod.Identifier);
            }
        }

        public IEnumerable<IModdable> GetBroadcastTargets() => GetPotentialBroadcastTargets().Where(x => Mod.CanMod(x));


        public abstract IEnumerable<IModdable> GetPotentialBroadcastTargets();

        public void DelayedBroadcast ()
        {
            StartCoroutine(InternalDelayedBroadcast());
        }

        private IEnumerator InternalDelayedBroadcast()
        {
            yield return new WaitForEndOfFrame();
            BroadcastMod();
        }
    }
}
