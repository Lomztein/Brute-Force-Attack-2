using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References.PrefabProviders;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Research;
using Lomztein.BFA2.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab.PrefabProviders
{
    public class IdentifiableUnlockPrefabProvider : MonoBehaviour, ICachedPrefabProvider, IDynamicProvider<IContentCachedPrefab>
    {
        public string ContentPath;
        public string UnlockListName;
        public int MissingResearchThreshold;

        private IUnlockList UnlockList => Player.Player.Unlocks; // TODO: Replace with something like a PlayerUnlockListLink.

        private IContentCachedPrefab[] _allPrefabs;

        public event Action<IEnumerable<IContentCachedPrefab>> OnAdded;
        public event Action<IEnumerable<IContentCachedPrefab>> OnRemoved;

        private void Awake()
        {
            _allPrefabs = ContentSystem.Content.GetAll<IContentCachedPrefab>(ContentPath).ToArray();
        }

        private void Start()
        {
            UnlockList.OnUnlockChange += OnUnlockChange;
        }

        private void OnUnlockChange(string identifier, bool value)
        {
            if (value == true)
            {
                IEnumerable<IContentCachedPrefab> newlyUnlocked = _allPrefabs
                    .Where(x => x.GetCache().GetComponent<Structure>().Identifier.Equals(identifier))
                    .Where(x => IsPartiallyUnlocked(x.GetCache().GetComponent<Structure>()));

                OnAdded?.Invoke(newlyUnlocked);
            }
        }


        private bool IsPartiallyUnlocked(Structure structure)
        {
            var missingResearch = UnlockList.GetRequiredResearchToUnlock(ResearchController.Instance.GetAll(), new[] { structure.Identifier }).Distinct().ToArray();
            return missingResearch.Length <= MissingResearchThreshold;
        }

        private IContentCachedPrefab[] GetPartiallyUnlocked()
        {
            return _allPrefabs.Where(x => IsPartiallyUnlocked(x.GetCache().GetComponent<Structure>())).ToArray();
        }

        public IContentCachedPrefab[] Get()
            => GetPartiallyUnlocked();
    }
}
