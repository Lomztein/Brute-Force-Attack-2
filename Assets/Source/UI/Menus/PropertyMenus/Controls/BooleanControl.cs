using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus.PropertyMenus.Controls
{
    public class BooleanControl : PropertyControl
    {
        public Toggle Toggle;

        public override bool CanControl(PropertyDefinition def) => def is BooleanDefinition;

        protected override void ControlImpl(PropertyDefinition def)
        {
            Toggle.isOn = (bool)def.DefaultValue;
            Toggle.onValueChanged.AddListener((x) => InvokeOnChanged(x));
        }

        public override object GetValue() => Toggle.isOn;

        public override void SetValue(object value) => Toggle.isOn = (bool)value;
    }
}