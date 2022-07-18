using Lomztein.BFA2.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI
{
    public class AbilityBar : MonoBehaviour
    {
        public AbilityManager Manager;
        public RectTransform LayoutParent;
        public GameObject AbilityButtonPrefab;

        private void Start()
        {
            if (Manager == null) Manager = AbilityManager.Instance;

            Manager.OnAbilityAdded += Manager_OnAbilityAdded;
            Manager.OnAbilityRemoved += Manager_OnAbilityRemoved;

            RegenerateButtons(Manager.CurrentAbilities);
        }

        private void OnDestroy()
        {
            Manager.OnAbilityAdded -= Manager_OnAbilityAdded;
            Manager.OnAbilityRemoved -= Manager_OnAbilityRemoved;
        }

        private void Manager_OnAbilityRemoved(string arg1, object arg2)
        {
            RegenerateButtons(Manager.CurrentAbilities);
        }

        private void Manager_OnAbilityAdded(Ability arg1, object arg2)
        {
            RegenerateButtons(Manager.CurrentAbilities);
        }

        public void RegenerateButtons (IEnumerable<Ability> abilities)
        {
            foreach (Transform child in LayoutParent)
            {
                Destroy(child.gameObject);
            }

            foreach (Ability ability in abilities)
            {
                GameObject newButton = Instantiate(AbilityButtonPrefab, LayoutParent);
                AbilityButton button = newButton.GetComponent<AbilityButton>();
                button.Assign(ability);
            }
        }
    }
}
