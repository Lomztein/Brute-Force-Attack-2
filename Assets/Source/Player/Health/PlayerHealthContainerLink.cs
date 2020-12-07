using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Health
{
    public class PlayerHealthContainerLink : MonoBehaviour, IHealthContainer
    {
        public event Action<float, float, float> OnHealthChanged {
            add {
                GetPlayerHealthContainer().OnHealthChanged += value;
            }

            remove {
                GetPlayerHealthContainer().OnHealthChanged -= value;
            }
        }

        public event Action OnHealthExhausted {
            add {
                GetPlayerHealthContainer().OnHealthExhausted += value;
            }

            remove {
                GetPlayerHealthContainer().OnHealthExhausted -= value;
            }
        }

        private IHealthContainer GetPlayerHealthContainer() => Player.Health;

        public float GetCurrentHealth()
        {
            return GetPlayerHealthContainer().GetCurrentHealth();
        }

        public float ChangeHealth(float amount)
        {
            return GetPlayerHealthContainer().ChangeHealth(amount);
        }

        public float GetMaxHealth() => GetPlayerHealthContainer().GetMaxHealth();
    }
}
