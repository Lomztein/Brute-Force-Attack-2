using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public abstract class ModBroadcaster : MonoBehaviour
    {
        [ModelProperty] public EmbeddedValueModel Mod = new EmbeddedValueModel();
        public IMod CachedMod => Mod.GetCache<IMod>();
        private List<IModdable> _currentBroadcastTargets = new List<IModdable>();

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
            if (CachedMod.IsCompatableWith(moddable))
            {
                moddable.Mods.AddMod(Mod.GetNew<IMod>());
                return true;
            }
            return false;
        }

        private void RemoveMod (IModdable moddable)
        {
            moddable.Mods.RemoveMod(CachedMod.Identifier);
        }

        protected abstract IEnumerable<IModdable> GetBroadcastTargets();

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
