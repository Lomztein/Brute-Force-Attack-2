using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Globals;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class GlobalModReward : CompletionReward
    {
        [ModelProperty]
        public string TargetGlobalModManager;
        private IMod _mod;

        public override void ApplyReward()
        {
            GlobalMod gmod = new GlobalMod(x => Fits(x), TargetGlobalModManager, GetMod());
            GlobalMods.Instance.TakeMod(gmod);
        }

        private IMod GetMod ()
        {
            if (_mod == null)
            {
                _mod = transform.Find("Mod").GetComponent<IMod>();
            }
            return _mod;
        }

        private bool Fits (IModdable moddable)
        {
            bool fits = GetMod().CompatableWith(moddable.Attributes);
            return fits;
        }
    }
}