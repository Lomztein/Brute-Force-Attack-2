﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Placement
{
    public interface IPlacement
    {
        void Init();

        bool Finish();
        event Action OnFinished;
    }
}
