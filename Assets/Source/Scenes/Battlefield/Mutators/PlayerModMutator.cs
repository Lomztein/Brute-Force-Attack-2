using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public class PlayerModMutator : Mutator
    {
        [ModelProperty, SerializeReference, SR]
        public IMod Mod;

        public override void Start()
        {
            Player.Player.Mods.AddMod(Mod);
        }
    }
}
