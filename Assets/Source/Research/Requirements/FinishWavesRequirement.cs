using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Requirements
{
    public class FinishWavesRequirement : CompletionRequirement
    {
        public override float Progress => _completed / (float)Target;

        public override event Action<CompletionRequirement> OnCompleted;
        public override event Action<CompletionRequirement> OnProgressed;

        [ModelProperty]
        public int Target;
        private int _completed;

        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        public override void Init()
        {
            _roundController.IfExists(x => x.OnWaveFinished += OnWaveFinished);
        }

        public override void Stop()
        {
            _roundController.IfExists(x => x.OnWaveFinished -= OnWaveFinished);
        }

        private void OnWaveFinished (int index, IWave wave)
        {
            _completed++;
            OnProgressed?.Invoke(this);

            if (_completed == Target)
            {
                OnCompleted?.Invoke(this);
            }
        }
    }
}
