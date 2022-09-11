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
        float ChangeHealth(float amount, object source);

        event Action<float, float, float, object> OnHealthChanged;
        event Action<object> OnHealthExhausted;
    }
}