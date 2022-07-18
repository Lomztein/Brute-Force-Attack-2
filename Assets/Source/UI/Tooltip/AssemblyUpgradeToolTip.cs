using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Structures.Turrets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class AssemblyUpgradeToolTip : MonoBehaviour
    {
        public Text Title;
        public Text ResearchRequired;
        public CostSheetDisplay CostDisplay;
        public MissingResearchDisplay ResearchDisplay;

        private IUnlockList List => Player.Player.Unlocks;

        public void AssignAssemblyUpgrade(TurretAssembly assembly, Tier tier)
        {
            Title.text = "Upgrade to " + tier.Name;
            var cost = assembly.GetCost(tier).Subtract(assembly.GetCost(assembly.CurrentTeir));
            CostDisplay.Display(cost);
            if (ResearchDisplay.Display(List, assembly.GetComponents(tier).Select(x => x.Identifier)) == 0)
            {
                ResearchRequired.gameObject.SetActive(false);
                ResearchDisplay.gameObject.SetActive(false);
            }
        }
    }
}
