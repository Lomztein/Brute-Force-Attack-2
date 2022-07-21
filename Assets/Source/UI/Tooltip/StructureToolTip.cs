using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI.Displays.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class StructureToolTip : MonoBehaviour, IPurchasableToolTip
    {
        public Text Title;
        public Text Description;
        public CostSheetDisplay CostSheet;
        public StatSheet StatSheet;

        public void Assign(IPurchasable obj)
        {
            Title.text = obj.Name;
            Description.text = obj.Description;
            if (string.IsNullOrEmpty(obj.Description))
            {
                Description.gameObject.SetActive(false);
            }
            if (obj is TurretAssembly assembly)
            {
                StatSheet.SetTarget(assembly.GetTierParent(assembly.CurrentTeir).gameObject);
                CostSheet.Display(assembly.GetCost(Tier.Initial));
            }
            else
            {
                StatSheet.SetTarget((obj as Component).gameObject);
                CostSheet.Display(obj.Cost);
            }
        }
    }
}
