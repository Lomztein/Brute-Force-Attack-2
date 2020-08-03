using Lomztein.BFA2.UI.Tooltip;
using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Research.UI
{
    public class ResearchMenu : MonoBehaviour, IWindow
    {
        public GameObject OptionButtonPrefab;
        public Transform OptionButtonParent;

        public event Action OnClosed;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void Init()
        {
            ResearchOption[] available = ResearchController.Instance.GetAvailable();
            foreach (var option in available)
            {
                CreateButton(option);
            }
        }

        public void CreateButton (ResearchOption option)
        {
            GameObject newButton = Instantiate(OptionButtonPrefab, OptionButtonParent);
            newButton.GetComponent<Button>().onClick.AddListener(() => BeginResearch(option));
            newButton.transform.Find("Image").GetComponentInChildren<Image>().sprite = option.Sprite.Get();
            newButton.transform.Find("Image").GetComponentInChildren<Image>().color = option.SpriteTint;
            newButton.GetComponentInChildren<Text>().text = option.Name;
            newButton.GetComponent<Tooltip>().SetTooltip(option.Name, option.Description 
                + "\n\nRequirements:\n" + string.Join("\n", option.GetRequirementDescriptions())
                + "\n\n Rewards:\n" + string.Join("\n", option.GetRewardDescriptions())
                , option.InitialCost.ToString());
            // TODO: Turn this into a dedicated ResearchMenuButton behaviour later, am lazy right now.
        }

        private void BeginResearch(ResearchOption option)
        {
            ResearchController.Instance.BeginResearch(option);
            Close();
        }
    }
}
