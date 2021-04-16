using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.UpcomingEnemies
{
    public interface IWaveDisplay
    {
        bool CanDisplay(IWave wave);
        void Display(IWave wave);
    }
}