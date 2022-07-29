using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class TimedFlag
    {
        public float Timeout { get; set; }

        private float _setTime;
        private bool _value;

        public TimedFlag(float trueTime)
        {
            Timeout = trueTime;
        }

        public TimedFlag() : this(0.1f) { }

        public void Mark()
        {
            _setTime = UnityEngine.Time.unscaledTime;
            _value = true;
        }

        public bool Get()
        {
            if (_setTime + Timeout < UnityEngine.Time.unscaledTime)
            {
                _value = false;
            }
            return _value;
        }
    }
}
