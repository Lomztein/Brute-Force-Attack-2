using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ToolTip
{
    public interface ITooltipUpdater
    {
        IHasToolTip GetTooltip();
    }
}
