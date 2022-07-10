using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    [CreateAssetMenu(fileName = "NewWaveCollection", menuName = "BFA2/Enemies/Waves/Premade Wave Collection")]
    public class PremadeWaveCollection : WaveCollection
    {
        [ModelProperty]
        public WaveTimeline[] Waves;

        public override WaveTimeline GetWave(int index)
        {
            return Waves[index];
        }

        public override int GetWaveCount()
        {
            return Waves.Length;
        }
    }
}
