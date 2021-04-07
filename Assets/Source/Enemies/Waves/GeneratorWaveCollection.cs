using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Enemies.Waves.Generators;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lomztein.BFA2.Enemies.Waves
{
    [Serializable]
    public class GeneratorWaveCollection : IWaveCollection
    {
        [ModelProperty]
        public ContentPrefabReference Spawner;

        [ModelProperty]
        public int Seed;
        [ModelProperty]
        public float StartingCredits;
        [ModelProperty]
        public float CreditsCoeffecient;

        [ModelProperty]
        public float StartingFrequency;
        [ModelProperty]
        public float FrequencyCoeffecient;

        [ModelProperty]
        public float MaxSequenceDenominator;
        [ModelProperty]
        public float MaxParallelDenominator;
        [ModelProperty]
        public int MaxSequence = 5;
        [ModelProperty]
        public float MaxParallel = 3;

        private readonly Dictionary<int, IWave> _waves = new Dictionary<int, IWave>();

        [ModelProperty]
        public string Identifier { get; set; }

        public IWave GetWave(int index)
        {
            if (_waves.ContainsKey(index))
            {
                return _waves[index];
            }
            else
            {
                Seed = Random.Range(0, short.MaxValue);
                //IWaveGenerator generator = new WaveGenerator(Spawner, Seed + index, GetAvailableCredits(index), GetSpawnFrequency(index), 200, 0.5f);
                IWaveGenerator generator = new CompositeWaveGenerator(Spawner, index, Seed + index, GetAvailableCredits(index), GetSpawnFrequency(index), GetMaxSequence(index), GetMaxParallel(index));
                IWave wave = generator.GenerateWave();
                _waves.Add(index, wave);
                return wave;
            }
        }

        private float GetSpawnFrequency(int wave) => StartingFrequency * Mathf.Pow(FrequencyCoeffecient, wave - 1);

        private float GetAvailableCredits(int wave) => StartingCredits * Mathf.Pow(CreditsCoeffecient, wave - 1);

        private int GetMaxSequence(int wave) => (int)Mathf.Min (Mathf.Max(1, Mathf.Round(wave / MaxSequenceDenominator)), MaxSequence);
        private int GetMaxParallel(int wave) => (int)Mathf.Min (Mathf.Max(1, Mathf.Round(wave / MaxParallelDenominator)), MaxParallel);
    }
}
