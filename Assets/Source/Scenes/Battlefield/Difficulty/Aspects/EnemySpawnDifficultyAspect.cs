using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.Difficulty.Aspects
{
    public class EnemySpawnDifficultyAspect : IDifficultyAspect, IHasProperties
    {
        public string Name => "Enemy spawns";
        public string Description => "Multipliers that scale enemy spawn amount and frequency.";
        public string CategoryIdentifier => "Core.Enemies";

        [ModelProperty]
        public float AmountMultiplier = 1f;
        [ModelProperty]
        public float WaveLengthMultiplier = 1f;

        public void Apply()
        {
            RoundController.Instance.OnNewWave += OnNewWave;
        }

        private void OnNewWave(Enemies.Waves.WaveTimeline obj)
        {
            obj.ForEach(x => x.Amount = Mathf.RoundToInt(x.Amount * AmountMultiplier));
            obj.ForEach(x => x.Length *= WaveLengthMultiplier);
        }

        public void AddPropertiesTo(PropertyMenu menu)
        {
            menu.AddProperty(new NumberDefinition("Enemy Amount Scalar", AmountMultiplier, false, 0.1f, 10f)).OnValueChanged += OnAmountMultChanged;
            menu.AddProperty(new NumberDefinition("Wave Length Scaler", WaveLengthMultiplier, false, 0.1f, 10f)).OnValueChanged += OnWaveLengthMultChanged;
        }

        private void OnWaveLengthMultChanged(object obj)
        {
            WaveLengthMultiplier = float.Parse(obj.ToString());
        }

        private void OnAmountMultChanged(object obj)
        {
            AmountMultiplier = float.Parse(obj.ToString());
        }
    }
}
