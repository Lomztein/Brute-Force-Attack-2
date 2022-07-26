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
            var missingResearch = list.GetRequiredResearchToUnlock(ResearchController.Instance.GetAll(), identifiers).Distinct();
            foreach (var research in missingResearch)
            {
                GameObject newObj = Instantiate(Prefab, Parent);
                newObj.transform.Find("Image").GetChild(0).GetComponent<Image>().sprite = research.Sprite.Get();

                if (ResearchController.Instance.GetAvailable().Contains(research))
                {
                    newObj.GetComponentInChildren<Text>().text = research.Name;
                    newObj.transform.Find("Image").GetChild(0).GetComponent<Image>().color = research.SpriteTint;
                    newObj.transform.SetAsFirstSibling();
                }
                else
                {
                    newObj.GetComponentInChildren<Text>().text = "???";
                    newObj.transform.Find("Image").GetChild(0).GetComponent<Image>().color = Color.black;
                    newObj.transform.SetAsLastSibling();
                }
            }
            return missingResearch.Count();
        }
    }
}
