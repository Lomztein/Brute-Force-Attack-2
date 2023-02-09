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
        [ModelProperty]
        public int Seed;
        [ModelProperty]
        public int MaxWaves = 100;

        [ModelProperty]
        public float StartingFrequency;
        [ModelProperty]
        public float FrequencyCoeffecient;

        [ModelProperty]
        public float StartingLength = 30;
        [ModelProperty]
        public float LengthPerWave = 3;
        [ModelProperty]
        public float MaxWaveLength = 60;

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
        public float SequenceMinLength;

        [ModelProperty]
        public Vector2 LengthVarianceMult;
        [ModelProperty]
        public Vector2 FrequencyVarianceMult;
        [ModelProperty]
        public Vector2 SubwaveVarianceVariance;

        private IWaveGenerator _generator = new WaveGenerator();

        private System.Random _random;
        private int _offset;

        private readonly Dictionary<int, WaveTimeline> _waves = new Dictionary<int, WaveTimeline>();

        public override WaveTimeline GetWave(int index)
        {
            if (_waves.ContainsKey(index))
            {
                return _waves[index];
            }
            else if (_generator.CanGenerate(index) && index < MaxWaves)
            {
                WaveTimeline wave = GenerateWave(index);
                _waves.Add(index, wave);
                return wave;
            }
            else return null;
        }

        private float GetSpawnFrequency(int wave) => StartingFrequency * Mathf.Pow(FrequencyCoeffecient, wave - 1);
        private float GetWaveTime(int wave) => Mathf.Min(StartingLength + LengthPerWave * wave, MaxWaveLength);
        private float GetAvailableCredits(int wave) => GetSpawnFrequency(wave) * GetWaveTime(wave);

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
            AddSequentialWaves(wave, timeline, ref wave);

            timeline.Rewarder = new FractionalWaveRewarder(timeline.Amount, GetCompletionReward(wave), GetEarnedFromKills(wave));
            timeline.Punisher = new FractionalWavePunisher(timeline.Amount);

            return timeline;
        }

        private float RandomRange(float min, float max) => Mathf.Lerp(min, max, (float)GetRandom().NextDouble());

        private void AddSequentialWaves(int wave, WaveTimeline timeline, ref int offset)
        {
            float waveTime = GetWaveTime(wave) * (1 + RandomRange(LengthVarianceMult.x, LengthVarianceMult.y));
            float frequency = GetSpawnFrequency(wave);
            int waves = GetSequenceAmount(wave);
            float[,] times = GenerateSequentialWaveTimes(waveTime, waves);

            for (int i = 0; i < times.GetLength(0); i++)
            {
                GenerateParallelWaves(timeline, times[i, 0], times[i, 1], frequency, wave, ref offset);
            }
        }

        private float[,] GenerateSequentialWaveTimes (float totalLength, int numWaves)
        {
            bool valid = true;
            float[] splits = new float[numWaves - 1];

            do
            {
                for (int i = 0; i < splits.Length; i++)
                {
                    splits[i] = RandomRange(0f, totalLength);
                }
                Array.Sort(splits);

                float cur = 0;
                valid = true;
                for (int i = 0; i < splits.Length; i++)
                {
                    if (splits[i] - cur < SequenceMinLength)
                    {
                        valid = false;
                    }
                    cur = splits[i];
                }
            }while (valid == false);

            float[,] times = new float[numWaves, 2];
            float t = 0;
            for (int i = 0; i < splits.Length; i++)
            {
                times[i, 0] = t;
                times[i, 1] = splits[i];
                t = splits[i];
            }
            if (splits.Length > 0)
            {
                times[splits.Length, 0] = splits[splits.Length - 1];
                times[splits.Length, 1] = totalLength - splits[splits.Length - 1];
            }
            else
            {
                times[0, 0] = 0f;
                times[0, 1] = totalLength;
            }
            return times;
        }

        private void GenerateParallelWaves(WaveTimeline timeline, float startTime, float length, float frequency, int wave, ref int offset)
        {
            int waves = GetParallelAmount(wave);
            for (int i = 0; i < waves; i++)
            {
                float waveFrequency = frequency * (1 + RandomRange(FrequencyVarianceMult.x, FrequencyVarianceMult.y));
                float waveLength = length * 1 + RandomRange(SubwaveVarianceVariance.x, SubwaveVarianceVariance.y);

                SpawnInterval interval = _generator.Generate(startTime, waveLength, waveFrequency, wave, Seed + offset++);
                timeline.AddSpawn(interval);
            }
        }

        public override int GetWaveCount()
        {
            int wave = 0;
            while (GetWave(wave) != null)
            {
                wave++;
            }
            return wave;
        }
    }
}
