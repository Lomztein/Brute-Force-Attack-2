using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Settings
{
    [CreateAssetMenu(fileName = "New Dropdown Setting", menuName = "BFA2/Settings/Dropdown")]
    public class DropdownSetting : Setting
    {
        [ModelProperty]
        public int DefaultValue = 0;
        [ModelProperty]
        public string[] Options;

        protected override object GetDefaultValue()
            => DefaultValue;
    }
}
