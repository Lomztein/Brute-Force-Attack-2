using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Stats.Elements
{
    public class CostStatSheetElement : StatSheetElementBase
    {
        public Resource Type;

        public override bool UpdateDisplay(GameObject target)
        {
            IPurchasable[] purchasables = target.GetComponentsInChildren<IPurchasable>();
            
            if (purchasables.Length > 0)
            {
                IResourceCost cost = purchasables.Select(x => x.Cost).Sum();
                if (cost.GetCost().TryGetValue(Type, out int value))
                {
                    SetText($"{value} {ResourceInfo.Get(Type).Shorthand}");
                    gameObject.SetActive(true);
                    return true;
                }
                else
                {
                    gameObject.SetActive(false);
                    return false;
                }
            }
            else
            {
                gameObject.SetActive(false);
                return false;
            }
        }
    }
}
