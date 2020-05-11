using System;
using System.Collections;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.FireControl
{
    public class SequenceFireControl : MonoBehaviour, IFireControl
    {
        public float DelayDenominator;

        public void Fire(int amount, float duration, Action<int> callback)
        {
            StartCoroutine (FireInternal(amount, duration, callback));
        }

        private IEnumerator FireInternal (int amount, float duration, Action<int> callback)
        {
            float delay = duration * DelayDenominator;
            for (int i = 0; i < amount; i++)
            {
                callback(i);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
