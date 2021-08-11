using System;
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
        protected override bool CanUpgrade()
        {
            return true;
        }

        protected override string GetStatus()
        {
            return string.Empty;
        }

        protected override bool ShowUpgrade()
        {
            return true;
        }

        protected override bool Upgrade()
        {
            return true;
        }
    }
}
