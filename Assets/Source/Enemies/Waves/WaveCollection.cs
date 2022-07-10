using Lomztein.BFA2.Serialization;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    public abstract class WaveCollection : ScriptableObject
    {   
        [ModelProperty]
        public string Identifier;

        public abstract WaveTimeline GetWave(int index);

        public abstract int GetWaveCount();
    }
}