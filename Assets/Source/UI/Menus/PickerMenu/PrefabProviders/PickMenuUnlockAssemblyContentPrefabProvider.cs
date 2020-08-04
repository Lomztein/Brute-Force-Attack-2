﻿using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References.PrefabProviders;
using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.PrefabProviders
{
    public class PickMenuUnlockAssemblyContentPrefabProvider : MonoBehaviour, ICachedPrefabProvider
    {
        public string ContentPath;
        public string UnlockListName;

        private IUnlockList UnlockList => UnlockLists.Get(UnlockListName);

        private IContentCachedPrefab[] _allPrefabs;
        private PickMenu _pickMenu;

        private void Awake()
        {
            _pickMenu = GetComponent<PickMenu>();
            _allPrefabs = Content.Content.GetAll(ContentPath, typeof(ContentCachedTurretAssemblyPrefab)).Cast<IContentCachedPrefab>().ToArray();
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
                    .Where(x => ContainsComponent(x.GetCache().GetComponent<ITurretAssembly>(), identifier))
                    .Where(x => IsUnlocked(x.GetCache().GetComponent<ITurretAssembly>()));

                _pickMenu.AddPickables(newlyUnlocked);
            }
        }

        private bool ContainsComponent(ITurretAssembly assembly, string identifier) => assembly.GetComponents().Any(x => x.UniqueIdentifier == identifier);

        private bool IsUnlocked(ITurretAssembly assembly) => assembly.GetComponents().All(x => UnlockList.IsUnlocked(x.UniqueIdentifier));

        private IContentCachedPrefab[] GetUnlocked ()
        {
            return _allPrefabs.Where(x => IsUnlocked(x.GetCache().GetComponent<ITurretAssembly>())).ToArray();
        }

        public IContentCachedPrefab[] Get()
            => GetUnlocked();
    }
}
