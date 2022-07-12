using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class CreditImageButton : MonoBehaviour, IHasToolTip
    {
        public string Name;
        public string Description;
        public string WebsiteUrl;
        public string DisplayedWebsiteUrl;
        public string Footnote => string.IsNullOrEmpty(DisplayedWebsiteUrl) ? WebsiteUrl : DisplayedWebsiteUrl;

        private void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OpenUrl);
        }

        private void OpenUrl ()
        {
            Application.OpenURL(WebsiteUrl);
        }

        public GameObject GetToolTip()
        {
            return SimpleToolTip.InstantiateToolTip(Name, Description, Footnote);
        }
    }
}
