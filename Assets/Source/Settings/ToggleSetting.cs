using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Settings
{
    [CreateAssetMenu(fileName = "New Toggle Setting", menuName = "BFA2/Settings/Toggle")]
    public class ToggleSetting : Setting
    {
        [ModelProperty]
        public int DefaultValue = 0;

        protected override object GetDefaultValue()
            => DefaultValue;
    }
}
