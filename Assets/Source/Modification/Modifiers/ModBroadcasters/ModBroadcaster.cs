using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.StructureManagement;
using Lomztein.BFA2.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public abstract class ModBroadcaster : MonoBehaviour
    {
        [ModelProperty]
        [SerializeField, SerializeReference, SR]
        protected IMod _mod;
        private List<IModdable> _currentBroadcastTargets = new List<IModdable>();
        private ObjectCloner _cloner = new ObjectCloner();

        protected virtual bool BroadcastPostAssembled => false;

        public void OnPostAssembled ()
        {
            if (BroadcastPostAssembled)
            {
                //BroadcastMod();
            }
        }

        protected void BroadcastMod()
        {
            var newTargets = GetBroadcastTargets();
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
            StructureManager.OnStructureChanged += OnStructureChange;
            StructureManager.OnStructureRemoved += OnStructureChange;
        }

        protected virtual void OnDestroy ()
        {
            StructureManager.OnStructureAdded -= OnStructureChange;
            StructureManager.OnStructureChanged -= OnStructureChange;
            StructureManager.OnStructureRemoved -= OnStructureChange;
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
            if (_mod.IsCompatableWith(moddable))
            {
                moddable.Mods.AddMod(_cloner.Clone(_mod));
                return true;
            }
            return false;
        }

        private void RemoveMod (IModdable moddable)
        {
            moddable.Mods.RemoveMod(_mod.Identifier);
        }

        public abstract IEnumerable<IModdable> GetBroadcastTargets();

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
