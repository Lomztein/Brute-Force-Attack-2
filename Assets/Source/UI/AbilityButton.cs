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

        private IEnumerable<string> _unavailableReasons;

        public void Assign(Ability ability)
        {
            Ability = ability;
            AbilityImage.sprite = ability.Sprite.Get();
            Button.onClick.AddListener(OnClick);
        }

        public GameObject GetToolTip()
        {
            return SimpleToolTip.InstantiateToolTip(Ability.Name, Ability.Description, _unavailableReasons.Count() == 0 ? "Ready" : string.Join("\n", _unavailableReasons));
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
            Button.interactable = Ability.Available(out _unavailableReasons);
            AbilityImage.color = Button.interactable ? Color.white : new Color(0.5f, 0.5f, 0.5f);

    }
}
}
