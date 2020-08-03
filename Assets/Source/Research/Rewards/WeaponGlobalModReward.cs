using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Globals;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class WeaponGlobalModReward : CompletionReward
    {
        [ModelProperty]
        public Colorization.Color Color;

        private IMod _mod;
        private const string TargetGlobalModManager = "TurretComponent";

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
            => GetMod().CompatableWith(moddable.Attributes) && (moddable as TurretWeapon)?.Color == Color;
    }
}