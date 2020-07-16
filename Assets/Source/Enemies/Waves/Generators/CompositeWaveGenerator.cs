using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves.Generators
{
    public class CompositeWaveGenerator : IWaveGenerator
    {
        private readonly int _seed;
        private System.Random _random;

        private readonly GameObject _spawner;
        private readonly float _credits;
        private readonly float _frequency;
        private readonly int _maxSequence;
        private readonly int _maxParallel;

        private const float MaxSpawnFrequency = 200f;
        private const float MinSpawnFrequency = 0.5f;

        public CompositeWaveGenerator (GameObject spawner, int seed, float credits, float frequency, int maxSequence, int maxParallel)
        {
            _seed = seed;
            _spawner = spawner;
            _credits = credits;
            _frequency = frequency;
            _maxSequence = maxSequence;
            _maxParallel = maxParallel;
        }

        private System.Random GetRandom ()
        {
            if (_random == null)
            {
                _random = new System.Random(_seed);
            }
            return _random;
        }

        private int GetSequenceAmount() => GetRandom().Next(_maxSequence) + 1;
        private int GetParallelAmount() => GetRandom().Next(_maxParallel) + 1;

        public IWave GenerateWave()
        {
            return GenerateSequentialWave();
        }

        private IWave GenerateSequentialWave ()
        {
            IWave[] waves = new IWave[GetSequenceAmount()];
            for (int i = 0; i < waves.Length; i++)
            {
                waves[i] = GenerateParallelWave(_credits / waves.Length);
            }
            return new SequentialCompositeWave(waves);
        }

        private IWave GenerateParallelWave (float credits)
        {
            IWave[] waves = new IWave[GetParallelAmount()];
            int len = waves.Length;
            for (int i = 0; i < waves.Length; i++)
            {
                IWaveGenerator gen = new WaveGenerator(_spawner, _seed + i, credits / len, _frequency / len, MaxSpawnFrequency / len, MinSpawnFrequency / len);
                waves[i] = gen.GenerateWave();
            }

            return new ParallelCompositeWave(waves);
        }
    }
}
