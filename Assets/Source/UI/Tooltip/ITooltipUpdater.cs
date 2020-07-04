using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Tooltip
{
    public interface ITooltipUpdater
    {
        ITooltip GetTooltip();
    }
}
