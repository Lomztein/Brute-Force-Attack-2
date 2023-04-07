﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using UnityEngine;

namespace Lomztein.BFA2.Modification
{
    public class ModContainer : IModContainer
    {
        private IStatContainer _stats;
        private IEventContainer _events;
        private IModdable _parent;

        private List<Mod> _mods = new List<Mod>();
        public Mod[] Mods => _mods.ToArray();

        public ModContainer(IModdable parent, IStatContainer stats, IEventContainer events)
        {
            _parent = parent;
            _stats = stats;
            _events = events;
        }

        public void AddMod(Mod mod)
        {
            if (GetAmount(mod.Identifier) == 0)
            {
                mod.ApplyBase(_parent, _stats, _events);
            }
            else
            {
                mod.ApplyStack(_parent, _stats, _events);
            }
            _mods.Add(mod);
        }

        public void RemoveMod(string identifier)
        {
            if (GetAmount(identifier) != 0)
            {
                Mod mod = GetMod(identifier);
                if (GetAmount(identifier) == 1)
                {
                    mod.RemoveBase(_parent, _stats, _events);
                }
                else
                {
                    mod.RemoveStack(_parent, _stats, _events);
                }
                _mods.Remove(mod);
                UnityEngine.Object.Destroy(mod);
            }
        }

        private Mod GetMod(string identifier) => _mods.FirstOrDefault(x => x.Identifier == identifier);

        private int GetAmount (string identifier)
        {
            return _mods.Count(x => x.Identifier == identifier);
        }
    }
}
