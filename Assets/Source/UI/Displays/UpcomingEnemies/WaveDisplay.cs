using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays.UpcomingEnemies
{
    public class WaveDisplay : WaveDisplayBase<Wave>
    {
        public Image EnemyImage;
        public Text AmountText;

        private int _amount;
        private Wave _wave;

        public override void Display(Wave wave)
        {
            EnemyImage.sprite = Iconography.GenerateSprite(wave.Prefab.GetCache());
            _amount = wave.SpawnAmount;
            _wave = wave;
            UpdateAmount();

            _wave.OnEnemyKill += OnEnemyKilled;
            _wave.OnEnemyFinish += OnEnemyKilled;

            _wave.OnFinished += OnWaveFinished;
        }

        private void OnWaveFinished()
        {
            _wave.OnEnemyKill -= OnEnemyKilled;
            _wave.OnEnemyFinish -= OnEnemyKilled;
            _wave.OnFinished -= OnWaveFinished;
        }

        private void OnEnemyKilled(IEnemy enemy)
        {
            _amount--;
            UpdateAmount();
        }

        private void UpdateAmount()
        {
            if (AmountText)
            {
                AmountText.text = "x " + _amount.ToString();
            }
        }
    }
}
