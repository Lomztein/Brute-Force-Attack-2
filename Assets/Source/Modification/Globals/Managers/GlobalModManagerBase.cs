using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Globals.Managers
{
    public abstract class GlobalModManagerBase<T> : MonoBehaviour, IGlobalModManager where T : IModdable
    {
        public bool Fits(string identifier) => identifier == typeof(T).Name;

        public abstract void TakeMod(GlobalMod mod);

        public abstract void RemoveMod(GlobalMod mod);
    }
}
