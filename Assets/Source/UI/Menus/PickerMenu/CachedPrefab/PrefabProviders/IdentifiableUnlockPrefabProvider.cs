using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References.PrefabProviders;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Player.Progression;
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
        public string UnlockListName;
        public string ContentPath;
        private IUnlockList UnlockList => UnlockLists.Get(UnlockListName);

        public event Action<IEnumerable<IContentCachedPrefab>> OnAdded;
        public event Action<IEnumerable<IContentCachedPrefab>> OnRemoved;

        private List<IContentCachedPrefab> _prefabs = new List<IContentCachedPrefab>();

        public IContentCachedPrefab[] Get() => _prefabs.Where(x => IsUnlocked(x)).ToArray();

        private void Awake()
        {
            _prefabs.AddRange(ContentSystem.Content.GetAll<IContentCachedPrefab>(ContentPath));
        }

        private void Start()
        {
            UnlockList.OnUnlockChange += OnUnlockChanged;
        }

        private void OnUnlockChanged(string identifier, bool value)
        {
            IContentCachedPrefab prefab = _prefabs.FirstOrDefault(x => x.GetCache().GetComponent<IIdentifiable>().UniqueIdentifier == identifier);
            if (value == true)
            {
                Add(new[] { prefab });
            }
            else
            {
                Remove(new[] { prefab });
            }
        }

        private void Remove(IEnumerable<IContentCachedPrefab> prefabs)
        {
            OnRemoved?.Invoke(prefabs);
        }

        private void Add (IEnumerable<IContentCachedPrefab> prefabs)
        {
            OnAdded?.Invoke(prefabs);
        }

        private bool IsUnlocked(IContentCachedPrefab prefab) => UnlockList.IsUnlocked(prefab.GetCache().GetComponent<IIdentifiable>().UniqueIdentifier);
    }
}
