using Lomztein.BFA2.Abilities;
using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI
{
    public class AbilityButton : MonoBehaviour, IHasToolTip
    {
        public Button Button;
        public Ability Ability;
        public Image AbilityImage;
        public Image AbilityProgress;
        public Text ChargesText;


        public void Assign(Ability ability)
        {
            Ability = ability;
            AbilityImage.sprite = ability.Sprite.Get();
            Button.onClick.AddListener(OnClick);
        }

        public GameObject GetToolTip()
        {
            var reasons = Ability.GetUnavailableReasons();
            return SimpleToolTip.InstantiateToolTip(Ability.Name, Ability.Description, reasons.Count() == 0 ? "Ready" : string.Join("\n", reasons));
        }

        public void OnClick ()
        {
            Ability.Select();
        }

        private void Update()
        {
            UpdateProgress();
        }

        private void UpdateProgress ()
        {
            AbilityProgress.fillAmount = Ability.CooldownProgress;
            if (Ability.Charges > 1)
            {
                ChargesText.gameObject.SetActive(true);
                ChargesText.text = Ability.Charges.ToString();
            }
            else
            {
                ChargesText.gameObject.SetActive(false);
            }
            if (Ability.Charges > 0)
            {
                AbilityProgress.fillAmount = 0;
            }
            Button.interactable = Ability.IsAvailable();
            AbilityImage.color = Button.interactable ? Color.white : new Color(0.5f, 0.5f, 0.5f);

    }
}
}
