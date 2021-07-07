using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.Weaponary.FireControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class PresetFireControl : MonoBehaviour, IFireControl
    {
        [ModelProperty, SerializeField]
        public float[] Timings;

        public void Fire(int amount, float duration, Action<int> callback)
        {
            StartCoroutine(DoFire(duration, callback));
        }

        private IEnumerator DoFire (float duration, Action<int> callback)
        {
            for (int i = 0; i < Timings.Length; i++)
            {
                float time = Timings[i] * duration;
                yield return UnityUtils.WaitForFixedSeconds(time);
                callback(i);
            }
        }
    }
}
