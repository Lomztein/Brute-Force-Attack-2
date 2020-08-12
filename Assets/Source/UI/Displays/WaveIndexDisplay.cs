using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays
{
    public class WaveIndexDisplay : MonoBehaviour
    {
        public Text Text;

        public string DisplayText;
        public string CurrentDisplayText;

        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        private void Awake()
        {
            _roundController.IfExists(x =>
            {
                x.OnWaveStarted += OnWaveStarted;
                x.OnWaveFinished += OnWaveFinished;
                OnWaveFinished(x.CurrentWaveIndex, null);
            });
        }

        private void OnWaveFinished(int arg1, Enemies.Waves.IWave arg2)
        {
            Text.text = DisplayText.Replace("{0}", (arg1 + 1).ToString());
        }

        private void OnWaveStarted(int arg1, Enemies.Waves.IWave arg2)
        {
            Text.text = CurrentDisplayText.Replace("{0}", (arg1).ToString());
        }
    }
}
