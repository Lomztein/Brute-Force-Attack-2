using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lomztein.BFA2
{
    public class NotifyOnDestroy : MonoBehaviour
    {
        public UnityEvent Event;
        private void OnDestroy()
        {
            if (Application.isPlaying)
            {
                Event.Invoke();
            }
        }
    }
}
