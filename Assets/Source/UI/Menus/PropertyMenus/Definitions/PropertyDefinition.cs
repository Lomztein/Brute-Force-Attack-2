using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions
{
    public abstract class PropertyDefinition
    {
        public object DefaultValue;
        public string Name;

        public PropertyDefinition (string name, object defaultValue)
        {
            Name = name;
            DefaultValue = defaultValue;
        }
    }
}