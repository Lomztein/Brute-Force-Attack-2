using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Health
{
    public interface IHealthContainer
    {
        float GetMaxHealth();
        float GetCurrentHealth();
        float ChangeHealth(float amount);

        event Action<float, float, float> OnHealthChanged;
        event Action OnHealthExhausted;
    }
}