﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers
{
    public abstract class Mod : ScriptableObject
    {
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;

        public abstract float Coeffecient { get; set; }
        public abstract bool CanMod(IModdable moddable);

        public abstract void ApplyBase(IModdable moddable, IStatContainer stats, IEventContainer events);
        public abstract void ApplyStack(IModdable moddable, IStatContainer stats, IEventContainer events);
        public abstract void RemoveBase(IModdable moddable, IStatContainer stats, IEventContainer events);
        public abstract void RemoveStack(IModdable moddable, IStatContainer stats, IEventContainer events);

        public override string ToString()
        {
            return Identifier;
        }
    }
}
