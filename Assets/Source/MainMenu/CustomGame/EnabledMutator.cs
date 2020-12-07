using Lomztein.BFA2.Mutators;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.MainMenu.CustomGame
{
    public class EnabledMutator : MonoBehaviour, ITooltip
    {
        public Text Label;
        public Mutator Mutator { get; set;}

        public string Title => Mutator.Name;
        public string Description => Mutator.Description;
        public string Footnote => null;

        public Button DisableButton;
        public Button ExpandButton;

        public PropertyMenu Properties;

        public void Assign(Mutator mutator, Action<Mutator> onClickDisable)
        {
            Label.text = mutator.Name;
            Mutator = mutator;
            DisableButton.onClick.AddListener(() => onClickDisable(mutator));

            if (mutator is IHasProperties withProperties)
            {
                withProperties.AddProperties(Properties);
            }
            else
            {
                ExpandButton.gameObject.SetActive(false);
                Properties.gameObject.SetActive(false);
            }
        }

        public void ToggleExpand ()
        {
            Properties.gameObject.SetActive(!Properties.gameObject.activeSelf);
        }
    }
}
