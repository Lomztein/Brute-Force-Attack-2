using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
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

        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        private void Start()
        {
            _roundController.IfExists(x =>
            {
                x.OnNextWaveChanged += (y) => OnWaveStarted(y, RoundController.Instance.WaveCollection.GetWaveCount());
                OnWaveStarted(x.NextIndex, RoundController.Instance.WaveCollection.GetWaveCount());
            });
        }

        private void OnWaveStarted(int wave, int total)
        {
            Text.text = DisplayText.Replace("{0}", wave.ToString()).Replace("{1}", total.ToString());
        }
    }
}