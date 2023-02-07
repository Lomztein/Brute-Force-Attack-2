using Lomztein.BFA2.Purchasing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class StructureWorldToolTip : MonoBehaviour, IHasToolTip
    {
        public GameObject Prefab;

        public GameObject InstantiateToolTip()
        {
            GameObject newToolTip = Instantiate(Prefab);
            newToolTip.GetComponent<StructureToolTip>().Assign(GetComponent<IPurchasable>());
            return newToolTip;
        }
    }
}
