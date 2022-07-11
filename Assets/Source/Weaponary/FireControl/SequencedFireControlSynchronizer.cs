using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.FireSynchronization
{
    public class SequencedFireControlSynchronizer
    {
        private List<SequencedFireControl> _syncs = new List<SequencedFireControl>();
        public IEnumerable<SequencedFireControl> Syncs => _syncs;

        private int _currentIndex;
        private SequencedFireControl _next;

        private float _timeBetweenFires;

        private float Time => UnityEngine.Time.time;
        private float _nextFireTime;

        public SequencedFireControlSynchronizer (float timeBetweenFires)
        {
            _timeBetweenFires = timeBetweenFires;
        }

        public bool CanFire(SequencedFireControl sync)
            => sync == _next && _nextFireTime <= Time;

        public void OnFire (SequencedFireControl sync)
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

        public void AddSync (SequencedFireControl sync)
        {
            _syncs.Add(sync);
            if (_syncs.Count == 1)
            {
                _currentIndex = 0;
                _next = _syncs[_currentIndex];
            }
        }

        public void RemoveSync (SequencedFireControl sync)
        {
            _syncs.Remove(sync);
        }
    }
}
