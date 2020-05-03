using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    public class WaveCollection : IWaveCollection
    {
        private IWave[] _waves;

        public void Init(IWave[] waves)
        {
            _waves = waves;
        }

        public IWave GetWave(int index)
        {
            return _waves[index];
        }
    }
}
