using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class CreditImageButton : MonoBehaviour, ITooltip
    {
        public string Name;
        public string Description;
        public string WebsiteUrl;

        public string Title => Name;
        string ITooltip.Description => Description;
        public string Footnote => WebsiteUrl;

        private void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OpenUrl);
        }

        private void OpenUrl ()
        {
            Application.OpenURL(WebsiteUrl);
        }
    }
}
