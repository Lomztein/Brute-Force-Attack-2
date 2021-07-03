using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Structures.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Upgrading
{
    public class AssemblyUpgrader : Upgrader
    {
        public override IResourceCost Cost => ComputeCost();

        private IResourceCost ComputeCost()
        {
            TurretAssembly assembly = GetComponent<TurretAssembly>();
            int prev = assembly.CurrentTeir;
            int next = prev + 1;

            IResourceCost prevCost = assembly.GetCost(prev);
            IResourceCost nextCost = assembly.GetCost(next);
            return nextCost.Subtract(prevCost);
        }

        public override bool CanUpgrade()
        {
            TurretAssembly assembly = GetComponent<TurretAssembly>();
            return Player.Player.Resources.HasEnough(Cost) && assembly.CurrentTeir < assembly.TierAmount;
        }

        public override bool Upgrade()
        {
            TurretAssembly assembly = GetComponent<TurretAssembly>();
            if (Player.Player.Resources.TrySpend(Cost) && assembly.CurrentTeir < assembly.TierAmount)
            {
                assembly.SetTier(assembly.CurrentTeir + 1);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
