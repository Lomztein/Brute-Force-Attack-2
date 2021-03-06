﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Upgrading
{
    public class ModUpgrader : Upgrader
    {
        public override bool CanUpgrade()
        {
            return true;
        }

        public override string GetStatus()
        {
            return string.Empty;
        }

        public override bool ShowUpgrade()
        {
            return true;
        }

        public override bool Upgrade()
        {
            return true;
        }
    }
}
