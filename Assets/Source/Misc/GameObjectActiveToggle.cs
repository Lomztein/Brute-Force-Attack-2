using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Misc
{
    public class GameObjectActiveToggle : MonoBehaviour
    {
        event Action<bool> OnToggled;

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
            OnToggled?.Invoke(value);
        }

        public void Activate() => SetActive(true);
        public void Deactivate() => SetActive(false);

        public void DelayedSetActive (bool value, float time)
        {
            StartCoroutine(InternalDelayedSetActive(value, time));
        }

        public void DelayedActivate(float time) => DelayedSetActive(true, time);
        public void DelayedDeactivate(float time) => DelayedSetActive(false, time);

        IEnumerator InternalDelayedSetActive (bool value, float time)
        {
            yield return new WaitForSeconds(time);
            SetActive(value);
            OnToggled?.Invoke(value);
        }
    }
}
