using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Abilities
{
    public class CooldownAbilityGranter : MonoBehaviour
    {
        [ModelAssetReference]
        public Ability Ability;
        [ModelProperty]
        public int ChargesPerStack = 1;
        [ModelProperty]
        public bool GrantCharge;

        private bool _granted;

        private void Start()
        {
            GrantAbility();
            _granted = true;
        }

        private void OnDestroy()
        {
            if (_granted)
            {
                RemoveAbility();
            }
        }

        private void GrantAbility()
        {
            CooldownAbility existing = AbilityManager.Instance.GetAbility(Ability.Identifier) as CooldownAbility;
            if (existing != null)
            {
                existing.MaxCharges += ChargesPerStack;
                if (GrantCharge)
                {
                    existing.CurrentCharges += ChargesPerStack;
                }
            }
            else
            {
                AbilityManager.Instance.AddAbility(Instantiate(Ability), this);
            }
        }

        private void RemoveAbility()
        {
            CooldownAbility existing = AbilityManager.Instance.GetAbility(Ability.Identifier) as CooldownAbility;
            if (existing != null)
            {
                if (existing.MaxCharges > ChargesPerStack)
                {
                    existing.MaxCharges -= ChargesPerStack;
                    existing.CurrentCharges = Mathf.Min(existing.CurrentCharges, existing.MaxCharges);
                }
                else
                {
                    AbilityManager.Instance.RemoveAbility(Ability.Identifier, this);
                }
            }
        }
    }
}
