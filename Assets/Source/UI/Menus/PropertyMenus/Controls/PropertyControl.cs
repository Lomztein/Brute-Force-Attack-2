using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus.PropertyMenus.Controls
{
    public abstract class PropertyControl : MonoBehaviour
    {
        public Text PropertyNameText;
        public event Action<object> OnValueChanged;

        public abstract bool CanControl(PropertyDefinition def);

        public void Control(PropertyDefinition def)
        {
            PropertyNameText.text = def.Name;
            ControlImpl(def);
        }

        protected abstract void ControlImpl(PropertyDefinition def); // Don't think about the naming please.

        public abstract object GetValue();

        public abstract void SetValue(object value);

        protected void InvokeOnChanged(object value) => OnValueChanged?.Invoke(value);
    }
}