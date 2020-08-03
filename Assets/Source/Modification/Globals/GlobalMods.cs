using Lomztein.BFA2.Modification.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Globals
{
    public class GlobalMods : MonoBehaviour
    {
        public static GlobalMods Instance;

        private IGlobalModManager[] _managers;

        private void Awake()
        {
            Instance = this;
            _managers = GetComponents<IGlobalModManager>();
        }

        public void TakeMod (GlobalMod mod)
        {
            FindApplicable(mod)?.TakeMod(mod);
        }

        public void RemoveMod (GlobalMod mod)
        {
            FindApplicable(mod)?.RemoveMod(mod);
        }

        private IGlobalModManager FindApplicable(GlobalMod mod) => _managers.FirstOrDefault(x => x.Fits(mod.TargetType));
    }
}
