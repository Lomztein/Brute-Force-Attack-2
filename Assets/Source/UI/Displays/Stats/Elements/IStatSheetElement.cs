using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Stats.Elements
{
    public interface IStatSheetElement
    {
        bool UpdateDisplay(GameObject target);
    }
}
