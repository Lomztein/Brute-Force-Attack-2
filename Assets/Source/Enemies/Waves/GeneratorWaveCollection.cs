using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Enemies.Waves.Generators;
using Lomztein.BFA2.Enemies.Waves.Punishers;
using Lomztein.BFA2.Enemies.Waves.Rewarders;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lomztein.BFA2.Enemies.Waves
{
    [CreateAssetMenu(fileName = "NewWaveCollection", menuName = "BFA2/Enemies/Waves/Generator Wave Collection")]
    public class GeneratorWaveCollection : WaveCollection
    {
        private const float MaxSpawnFrequency = 200f;
        private const float MinSpawnFrequency = 2f;

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
        public float InitialLoCFromKills;
        [ModelProperty]
        public float LoCFromKillsPerWave;

        [ModelProperty]
        public float InitialLoCOnCompletion;
        [ModelProperty]
        public float LoCOnCompletionPerWave;

        [ModelProperty]
        public int SequenceMax = 10;
        [ModelProperty]
        public int ParallelMax = 3;

        [ModelProperty]
        public float SequenceDenomenator = 5;
        [ModelProperty]
        public float ParallelDenomenator = 5;

        [ModelProperty]
        public Vector2 CreditsVariance;
        public Vector2 FrequencyVariance;
        public Vector2 TimeVariance;

        private System.Random _random;

        private readonly Dictionary<int, WaveTimeline> _waves = new Dictionary<int, WaveTimeline>();

        public override WaveTimeline GetWave(int index)
        {
            if (_waves.ContainsKey(index))
            {
                return _waves[index];
            }
            else
            {
                WaveTimeline wave = GenerateWave(index);
                _waves.Add(index, wave);
                return wave;
            }
        }

        private float GetSpawnFrequency(int wave) => StartingFrequency * Mathf.Pow(FrequencyCoeffecient, wave - 1);
        private float GetAvailableCredits(int wave) => StartingCredits * Mathf.Pow(CreditsCoeffecient, wave - 1);
        private int GetSequenceAmount(int wave) => Mathf.Min(GetRandom().Next(1, Mathf.FloorToInt(wave / SequenceDenomenator + 1)), SequenceMax);
        private int GetParallelAmount(int wave) => Mathf.Min(GetRandom().Next(1, Mathf.FloorToInt(wave / ParallelDenomenator + 1)), ParallelMax);

        private float GetEarnedFromKills(int wave) => InitialLoCFromKills + LoCFromKillsPerWave * (wave - 1);
        private float GetCompletionReward(int wave) => InitialLoCOnCompletion + LoCOnCompletionPerWave * (wave - 1);

        private System.Random GetRandom()
        {
            if (_random == null)
            {
                _random = new System.Random(Seed);
            }
            return _random;
        }

        private WaveTimeline GenerateWave(int wave)
        {
            WaveTimeline timeline = new WaveTimeline();
            AddSequentialWaves(wave, timeline);

            timeline.Rewarder = new FractionalWaveRewarder(timeline.Amount, GetCompletionReward(wave), GetEarnedFromKills(wave));
            timeline.Punisher = new FractionalWavePunisher(timeline.Amount);

            return timeline;
        }

        private float RandomRange(float min, float max) => Mathf.Lerp(min, max, (float)_random.NextDouble());

        private void AddSequentialWaves(int wave, WaveTimeline timeline)
        {
            float time = 0f;
            int waves = GetSequenceAmount(wave);

            float credits = GetAvailableCredits(wave);

            for (int i = 0; i < waves; i++)
            {
                time = GenerateParallelWaves(timeline, time, wave, credits / waves, waves * i);
            }
        }

        private float GenerateParallelWaves(WaveTimeline timeline, float startTime, int wave, float credits, int offset)
        {
            float frequency = GetSpawnFrequency(wave);

            int waves = GetParallelAmount(wave);
            for (int i = 0; i < waves; i++)
            {
                float waveFrequency = frequency * (1 + RandomRange(FrequencyVariance.x, FrequencyVariance.y));
                float waveCredits = credits * (1 + RandomRange(CreditsVariance.x, CreditsVariance.y));

                IWaveGenerator gen = new WaveGenerator(startTime, wave, Seed + waves * i + offset, waveCredits / waves, waveFrequency / waves, MaxSpawnFrequency / waves, MinSpawnFrequency / waves);
                SpawnInterval interval = gen.Generate();
                timeline.AddSpawn(interval);
            }
            return timeline.EndTime + RandomRange(TimeVariance.x, TimeVariance.y);
        }
    }
}
