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
            return SimpleToolTip.InstantiateToolTip(Ability.Name, Ability.Description, Ability.CurrentCooldown > 0 ? "Cooldown: " + Ability.CurrentCooldown : "Ready");
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
            AbilityProgress.fillAmount = ((float)Ability.CurrentCooldown / Ability.MaxCooldown);
            if (Ability.MaxCharges > 1)
            {
                ChargesText.gameObject.SetActive(true);
                ChargesText.text = Ability.CurrentCharges.ToString();
            }
            else
            {
                ChargesText.gameObject.SetActive(false);
            }
            if (Ability.CurrentCharges > 0)
            {
                AbilityProgress.fillAmount = 0;
            }
            Button.interactable = Ability.CurrentCharges > 0;
        }
    }
}
