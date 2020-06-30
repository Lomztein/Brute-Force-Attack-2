﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Highlighters
{
    public interface IHighlighter
    {
        bool CanHighlight(Type componentType);

        void Highlight(Component component);

        void EndHighlight();
    }
}
