using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Research;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2
{
    public class MissingResearchDisplay : MonoBehaviour
    {
        public RectTransform Parent;
        public GameObject Prefab;

        public int Display(IUnlockList list, IEnumerable<string> identifiers)
        {
            var missingResearch = list.GetRequiredResearchToUnlock(ResearchController.Instance.GetAll(), identifiers);
            foreach (var research in missingResearch)
            {
                GameObject newObj = Instantiate(Prefab, Parent);
                newObj.GetComponentInChildren<Text>().text = research.Value.Name;
                newObj.transform.Find("Image").GetChild(0).GetComponent<Image>().sprite = research.Value.Sprite.Get();
                newObj.transform.Find("Image").GetChild(0).GetComponent<Image>().color = research.Value.SpriteTint;
            }
            return missingResearch.Count();
        }
    }
}
