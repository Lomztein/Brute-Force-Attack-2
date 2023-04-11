using Lomztein.BFA2.ContentSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Settings
{
    [CreateAssetMenu(fileName = "New User Content Pack Dropdown Setting", menuName = "BFA2/Settings/Autos/User Content Pack")]
    public class UserContentPackDropdownSetting : DropdownSetting
    {
        public override void Init()
        {
            base.Init();
            Options = ContentManager.Instance.FindContentPacks().Where(x => x is ContentPack).Select(x => x.Name).ToArray();
        }

        protected override object GetDefaultValue()
        {
            var packs = ContentManager.Instance.FindContentPacks().Where(x => x is ContentPack).ToArray();
            return Array.IndexOf(packs, packs.FirstOrDefault(x => x.Name == "Custom"));
        }
    }
}
