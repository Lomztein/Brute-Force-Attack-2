using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Enemies.Waves.Generators;
using Lomztein.BFA2.Serialization.Models.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Enemies.Waves
{
    [Serializable]
    public class GeneratorWaveCollection : IWaveCollection
    {
        public GameObject Spawner;

        public int Seed;
        public float StartingCredits;
        public float CreditsCoeffecient;

        public float StartingFrequency;
        public float FrequencyCoeffecient;

        public float MaxSequenceDenominator;
        public float MaxParallelDenominator;

        private readonly Dictionary<int, IWave> _waves = new Dictionary<int, IWave>();

        public IWave GetWave(int index)
        {
            if (_waves.ContainsKey(index))
            {
                return _waves[index];
            }
            else
            {
                IWaveGenerator generator = new CompositeWaveGenerator(Spawner, Seed + index, GetAvailableCredits(index), GetSpawnFrequency(index), GetMaxSequence(index), GetMaxParallel(index));
                IWave wave = generator.GenerateWave();
                _waves.Add(index, wave);
                return wave;
            }
        }

        private float GetSpawnFrequency(int wave) => StartingFrequency * Mathf.Pow(FrequencyCoeffecient, wave - 1);

        private float GetAvailableCredits(int wave) => StartingCredits * Mathf.Pow(CreditsCoeffecient, wave - 1);

        private int GetMaxSequence(int wave) => (int)Mathf.Max(1, Mathf.Round(wave / MaxSequenceDenominator));
        private int GetMaxParallel(int wave) => (int)Mathf.Max(1, Mathf.Round(wave / MaxParallelDenominator));
    }
}
