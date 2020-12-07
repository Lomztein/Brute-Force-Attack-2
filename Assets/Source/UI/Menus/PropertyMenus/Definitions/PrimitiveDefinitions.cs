using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions
{
    public class BooleanDefinition : PropertyDefinition
    {
        public BooleanDefinition(string name, bool defaultValue) : base(name, defaultValue)
        {
        }
    }

    public class StringDefinition : PropertyDefinition
    {
        public StringDefinition(string name, string defaultValue) : base(name, defaultValue)
        {
        }
    }
}