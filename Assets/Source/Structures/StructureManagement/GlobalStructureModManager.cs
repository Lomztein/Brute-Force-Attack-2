﻿using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.StructureManagement
{
    public class GlobalStructureModManager : MonoBehaviour
    {
        public static GlobalStructureModManager Instance;
        private List<GlobalStructureMod> _mods = new List<GlobalStructureMod>();
        private List<IStructureModder> _modders;

        private void Awake()
        {
            Instance = this;
            _modders = ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IStructureModder>(typeof(GenericStructureModder)).ToList();
            _modders.Add(new GenericStructureModder());
        }

        private IStructureModder GetModder(Structure structure) => _modders.FirstOrDefault(x => x.CanMod(structure));

        private void Start()
        {
            AttachToStructureManager();
        }

        private void AttachToStructureManager()
        {
            StructureManager.OnStructureAdded += OnStructureAdded;
            StructureManager.OnStructureChanged += OnStructureChanged;
            StructureManager.OnStructureRemoved += OnStructureRemoved;
        }

        private void OnDestroy()
        {
            StructureManager.OnStructureAdded -= OnStructureAdded;
            StructureManager.OnStructureChanged -= OnStructureChanged;
            StructureManager.OnStructureRemoved -= OnStructureRemoved;
        }

        public void AddMod (GlobalStructureMod mod)
        {
            _mods.Add(mod);

            foreach (Structure structure in StructureManager.GetStructures())
            {
                ApplyMod(structure, mod);
            }
        }

        public void RemoveMod (GlobalStructureMod mod)
        {
            _mods.Remove(mod);

            foreach (Structure structure in StructureManager.GetStructures())
            {
                RemoveMod(structure, mod);
            }
        }

        private void OnStructureRemoved(Structure obj)
        {
            RemoveMods(obj);
        }

        private void OnStructureChanged(Structure obj)
        {
            RefreshMods(obj);
        }

        private void OnStructureAdded(Structure obj)
        {
            ApplyMods(obj);
        }

        public void ApplyMods (Structure obj)
        {
            foreach (var mod in _mods)
            {
                ApplyMod(obj, mod);
            }
        }

        private void ApplyMod (Structure obj, GlobalStructureMod mod)
        {
            GetModder(obj).AddTo(obj, mod);
        }

        public void RemoveMods (Structure obj)
        {
            foreach (var mod in _mods)
            {
                RemoveMod(obj, mod);
            }
        }

        private void RemoveMod(Structure obj, GlobalStructureMod mod)
        {
            GetModder(obj).RemoveFrom(obj, mod);
        }

        public void RefreshMods (Structure obj)
        {
            RemoveMods(obj);
            ApplyMods(obj);
        }
    }
}
