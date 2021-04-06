using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Lomztein.BFA2.Misc
{
    [Obsolete]
    public class ExecuteEventOnInputKey : MonoBehaviour
    {
        public KeyCode Key;
        public UnityEvent Event;

        private void Update()
        {
            Debug.LogError("ExecuteEventOnInputKey is no longer supported.");
        }
    }
}
