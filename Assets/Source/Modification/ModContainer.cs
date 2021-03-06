﻿using System;
using System.Collections.Generic;
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

        private List<IMod> _mods = new List<IMod>();

        public ModContainer(IStatContainer stats, IEventContainer events)
        {
            _stats = stats;
            _events = events;
        }

        public void AddMod(IMod mod)
        {
            if (GetAmount(mod.Identifier) == 0)
            {
                mod.ApplyBase(_stats, _events);
            }
            else
            {
                mod.ApplyStack(_stats, _events);
            }
            _mods.Add(mod);
        }

        public void RemoveMod(string identifier)
        {
            if (GetAmount(identifier) != 0)
            {
                IMod mod = GetMod(identifier);
                if (GetAmount(identifier) == 1)
                {
                    mod.RemoveBase(_stats, _events);
                }
                else
                {
                    mod.RemoveStack(_stats, _events);
                }
                _mods.Remove(mod);
            }
        }

        private IMod GetMod(string identifier) => _mods.FirstOrDefault(x => x.Identifier == identifier);

        private int GetAmount (string identifier)
        {
            return _mods.Count(x => x.Identifier == identifier);
        }
    }
}
