using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.FireSynchronization
{
    public class SequencedFireSyncronizationController
    {
        private List<SequencedFireSynchronization> _syncs = new List<SequencedFireSynchronization>();

        private int _currentIndex;
        private SequencedFireSynchronization _next;

        private float _timeBetweenFires;

        private float Time => UnityEngine.Time.time;
        private float _nextFireTime;

        public SequencedFireSyncronizationController (float timeBetweenFires)
        {
            _timeBetweenFires = timeBetweenFires;
        }

        public bool CanFire(SequencedFireSynchronization sync)
            => sync == _next && _nextFireTime <= Time;

        public void OnFire (SequencedFireSynchronization sync)
        {
            Next();
        }

        private void Next()
        {
            _currentIndex++;
            _currentIndex %= _syncs.Count;
            _next = _syncs[_currentIndex];
            _nextFireTime = (_timeBetweenFires / _syncs.Count) + Time;
        }

        public void AddSync (SequencedFireSynchronization sync)
        {
            _syncs.Add(sync);
            if (_syncs.Count == 1)
            {
                _currentIndex = 0;
                _next = _syncs[_currentIndex];
            }
        }

        public void RemoveSync (SequencedFireSynchronization sync)
        {
            _syncs.Remove(sync);
        }
    }
}
