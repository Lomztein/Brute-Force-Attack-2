using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Settings.Autos
{
    [CreateAssetMenu(fileName = "New Resolutions Dropdown Setting", menuName = "BFA2/Settings/Autos/Resolution")]
    public class ResolutionsDropdownSetting : DropdownSetting
    {
        public override void Init()
        {
            base.Init();
            Options = Screen.resolutions.Select(x => $"{x.width}x{x.height} @ {x.refreshRate}hz").ToArray();
        }

        protected override object GetDefaultValue()
        {
            int value = Screen.resolutions.ToList().IndexOf(Screen.currentResolution);
            return value;
        }
    }
}
