using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus.PropertyMenus.Controls
{
    public class InputControl : PropertyControl
    {
        public InputField InputField;

        public override bool CanControl(PropertyDefinition def)
        {
            return (def is StringDefinition) ||
                (def is NumberDefinition);
        }

        protected override void ControlImpl(PropertyDefinition def)
        {
            InputField.text = def.DefaultValue.ToString();

            if (def is NumberDefinition numdef)
            {
                InputField.contentType = (numdef.IsInteger ? InputField.ContentType.IntegerNumber : InputField.ContentType.DecimalNumber);
                InputField.onEndEdit.AddListener((x) => ClampValue(float.Parse(x), numdef.Min, numdef.Max));
            }

            InputField.onEndEdit.AddListener((x) => InvokeOnChanged(x));
        }

        private void ClampValue (float value, float min, float max)
        {
            InputField.text = Mathf.Clamp(value, min, max).ToString(CultureInfo.InvariantCulture);
        }

        public override object GetValue()
        {
            return InputField.text;
        }

        public override void SetValue(object value)
        {
            InputField.text = value.ToString();
        }
    }
}