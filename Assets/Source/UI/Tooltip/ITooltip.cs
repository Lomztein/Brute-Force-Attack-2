﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Tooltip
{
    public interface ITooltip
    {
        string Title { get; }
        string Description { get; }
        string Footnote { get; }
    }
}
