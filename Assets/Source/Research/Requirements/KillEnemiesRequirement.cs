﻿using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Research.Requirements
{
    public class KillEnemiesRequirement : CompletionRequirement
    {
        [ModelProperty]
        public float Target;
        private float _current;
        [ModelProperty]
        public Colorization.Color[] TargetColors;

        public override float Progress => _current / Target;

        public override string Description => "Slay " + Target + " worth of " + FormatTargetColors() + " enemies.";
        public override string Status => Mathf.Round(_current) + " / " + Target + " " + FormatTargetColors() + " worth of enemies slayn.";

        public override event Action<CompletionRequirement> OnCompleted;
        public override event Action<CompletionRequirement> OnProgressed;

        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        public override void Init()
        {
            _roundController.IfExists((x) => x.OnEnemyKill += OnEnemyKill);
        }

        public override void Stop()
        {
            _roundController.IfExists((x) => x.OnEnemyKill -= OnEnemyKill);
        }
        private void OnEnemyKill(IEnemy enemy)
        {
            if (IsTargetColor(enemy))
            {
                _current += enemy.DifficultyValue;
                OnProgressed?.Invoke(this);
                if (_current >= Target)
                {
                    OnCompleted?.Invoke(this);
                }
            }
        }

        private bool IsTargetColor (IEnemy enemy)
        {
            if (TargetColors == null || TargetColors.Length == 0)
            {
                return true;
            }

            Enemy e = enemy as Enemy;
            return TargetColors.Contains(e.Color);
        }

        private string FormatTargetColors() => string.Join(", ", TargetColors.Select(x => x.ToString()));
    }
}