﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.World
{
    public interface IGridObject
    {
        Size Width { get; }
        Size Height { get; }
    }
}
