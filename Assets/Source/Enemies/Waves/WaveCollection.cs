using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    [Serializable]
    public class WaveCollection : IWaveCollection
    {
        [ModelProperty]
        public Wave[] Waves;
        [ModelProperty]
        public string Identifier { get; set; }

        public IWave GetWave(int index)
        {
            return Waves[index];
        }
    }
}
