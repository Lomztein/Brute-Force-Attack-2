using Lomztein.BFA2.ContentSystem.Objects;
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
    public class EnemyTypeDisplay : MonoBehaviour
    {
        public Image EnemyImage;
        public Text AmountText;

        private int _amount;
        private string _identifier;

        public void Display(WaveHandler handler, int amount, string enemyIdentifier)
        {
            _identifier = enemyIdentifier;
            IContentCachedPrefab enemy = Enemy.GetEnemy(_identifier);
            EnemyImage.sprite = Iconography.GenerateSprite(enemy.GetCache());

            _amount = amount;
            UpdateAmount();

            if (handler) // Bit janky but shut up
            {
                handler.OnEnemyKilled += OnEnemyKilled;
                handler.OnEnemyFinished += OnEnemyKilled;
                handler.OnAllEnemiesDone += OnWaveFinished;
            }
        }

        private void OnWaveFinished(WaveHandler handler)
        {
            handler.OnEnemyKilled -= OnEnemyKilled;
            handler.OnEnemyFinished -= OnEnemyKilled;
            handler.OnAllEnemiesDone -= OnWaveFinished;
        }

        private void OnEnemyKilled(WaveHandler handler, IEnemy enemy)
        {
            if (enemy.Identifier == _identifier)
            {
                _amount--;
                UpdateAmount();
            }
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
