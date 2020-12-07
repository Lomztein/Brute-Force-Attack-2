using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Testing
{
    public class PropertyMenuTest : MonoBehaviour
    {
        public PropertyMenu PropertyMenu;

        private void Start()
        {
            PropertyMenu.AddProperty(new BooleanDefinition("The Boolean", true)).OnValueChanged += ValueChanged;
            PropertyMenu.AddProperty(new StringDefinition("The Text", "feesh")).OnValueChanged += ValueChanged;
            PropertyMenu.AddProperty(new NumberDefinition("The Number", 42, true, 0, 66)).OnValueChanged += ValueChanged;
            PropertyMenu.AddProperty(new NumberDefinition("The Decimal", 6.66f, true, -3.14f, 128.5f)).OnValueChanged += ValueChanged;
        }

        private void ValueChanged(object obj)
        {
            Debug.Log("The value has changed: " + obj);
        }
    }
}
