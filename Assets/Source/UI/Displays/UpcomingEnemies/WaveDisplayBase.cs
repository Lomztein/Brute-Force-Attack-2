using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.UpcomingEnemies
{
    public abstract class WaveDisplayBase<T> : MonoBehaviour, IWaveDisplay where T : IWave
    {
        public bool CanDisplay(IWave wave) => wave.GetType() == typeof(T);

        public void Display(IWave wave) => Display((T)wave);
        public abstract void Display(T wave);

        public abstract void OnEnemyKilled(IEnemy enemy);
    }
}
