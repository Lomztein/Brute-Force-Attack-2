using Lomztein.BFA2.Purchasing.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2
{
    public class CostSheetDisplay : MonoBehaviour
    {
        public GameObject CostElementPrefab;
        public RectTransform CostElementParent;

        public void Display(IResourceCost cost)
        {
            var resourceCost = cost.ToResourceCost();
            foreach (var element in resourceCost.Elements)
            {
                GameObject newElementObject = Instantiate(CostElementPrefab, CostElementParent);
                newElementObject.GetComponentInChildren<Image>().sprite = element.Resource.Sprite.Get();
                newElementObject.GetComponentInChildren<Text>().text = element.Value + " " + element.Resource.Name;
            }
        }
    }
}
