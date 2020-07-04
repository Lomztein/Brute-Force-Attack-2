using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Health
{
    public class PlayerHealthContainerLink : MonoBehaviour, IHealthContainer
    {
        private IHealthContainer _cache;
        private const string PLAYER_HEALTH_CONTAINER_TAG = "PlayerHealthContainer";

        private IHealthContainer GetPlayerHealthContainer ()
        {
            if (_cache == null)
            {
                _cache = GameObject.FindGameObjectWithTag(PLAYER_HEALTH_CONTAINER_TAG).GetComponent<IHealthContainer>();
            }
            return _cache;
        }

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
