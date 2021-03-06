﻿using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Menus.PickerMenu
{
    public interface IPickHandler<T>
    {
        void Handle(T pick);
    }
}
