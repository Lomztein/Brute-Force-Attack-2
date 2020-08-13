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

        private string _enemyIdentifier;
        private int _amount;

        public override void Display(Wave wave)
        {
            EnemyImage.sprite = Iconography.GenerateSprite(wave.Prefab.GetCache());
            _enemyIdentifier = wave.Prefab.GetCache().GetComponent<IEnemy>().UniqueIdentifier;
            _amount = wave.SpawnAmount;
            UpdateAmount();
        }

        public override void OnEnemyKilled(IEnemy enemy)
        {
            if (enemy.UniqueIdentifier == _enemyIdentifier)
            {
                _amount--;
                UpdateAmount();
            }
        }

        private void UpdateAmount()
        {
            AmountText.text = "x " + _amount.ToString();
        }
    }
}
