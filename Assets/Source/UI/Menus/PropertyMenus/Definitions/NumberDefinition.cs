using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions
{
    public class NumberDefinition : PropertyDefinition
    {
        public bool IsInteger;

        public float Min;
        public float Max;

        public NumberDefinition(string name, float defaultValue, bool isInt, float min, float max) : base(name, defaultValue)
        {
            IsInteger = isInt;
            Min = min;
            Max = max;
        }
    }
}
