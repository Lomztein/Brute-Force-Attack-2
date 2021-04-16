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
    public class SequentialCompositeWaveDisplay : WaveDisplayBase<SequentialCompositeWave>
    {
        public GameObject[] WaveDisplays;
        public GameObject SeperatorPrefab;
        public Transform SubwaveParent;
        private GameObject GetWaveDisplay(IWave wave) => WaveDisplays.FirstOrDefault(x => x.GetComponent<IWaveDisplay>().CanDisplay(wave));

        public override void Display(SequentialCompositeWave wave)
        {
            int index = 0;
            foreach (IWave w in wave.Waves)
            {
                IWaveDisplay display = Instantiate(GetWaveDisplay(w), SubwaveParent).GetComponent<IWaveDisplay>();
                display.Display(w);

                if (index != wave.Waves.Length - 1)
                {
                    Instantiate(SeperatorPrefab, SubwaveParent);
                }

                index++;
            }
        }
    }
}
