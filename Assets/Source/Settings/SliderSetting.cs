using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Settings
{
    [CreateAssetMenu(fileName = "New Slider Setting", menuName = "BFA2/Settings/Slider Setting")]
    public class SliderSetting : Setting
    {
        [ModelProperty]
        public float DefaultValue;
        [ModelProperty]
        public Vector2 MinMax;
        [ModelProperty]
        public bool WholeNumbersOnly;
        [ModelProperty]
        public string ValueFormat;

        protected override object GetDefaultValue()
            => DefaultValue;
    }
}
