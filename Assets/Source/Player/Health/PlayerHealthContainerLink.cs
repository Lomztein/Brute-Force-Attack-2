using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Health
{
    public class PlayerHealthContainerLink : MonoBehaviour, IHealthContainer
    {
        public event Action<float, float, float, object> OnHealthChanged {
            add {
                GetPlayerHealthContainer().OnHealthChanged += value;
            }

            remove {
                GetPlayerHealthContainer().OnHealthChanged -= value;
            }
        }

        public event Action<object> OnHealthExhausted {
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

        public float ChangeHealth(float amount, object source)
        {
            return GetPlayerHealthContainer().ChangeHealth(amount, source);
        }

        public float GetMaxHealth() => GetPlayerHealthContainer().GetMaxHealth();
    }
}
