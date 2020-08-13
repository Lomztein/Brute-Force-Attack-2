using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.UpcomingEnemies
{
    public class MainUpcomingDisplay : MonoBehaviour
    {
        public UpcomingDisplay Display;
        public string UpcomingText = "Upcoming";
        public string CurrentText = "Current";

        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        public void Start()
        {
            _roundController.IfExists(x =>
            {
                x.OnWaveStarted += OnWaveStarted;
                x.OnWaveFinished += OnWaveFinished;

                OnWaveFinished(x.CurrentWaveIndex, null);
            });
        }

        private void OnWaveFinished(int index, IWave wave)
        {
            Display.SetTitle(UpcomingText);
            Display.Display(_roundController.Dependancy.GetWave(index + 1));
        }

        private void OnWaveStarted(int index, IWave wave)
        {
            Display.SetTitle(CurrentText);
        }
    }
}
