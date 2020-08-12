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
    public class NextWaveButtonDisplay : MonoBehaviour
    {
        public Color ReadyColor;
        public Color PreparingColor;
        public Color InProgressColor;

        public Image Image;
        public Button Button;

        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        private void Awake()
        {
            _roundController.IfExists(x =>
            {
                x.OnWavePreparing += OnWavePreparing;
                x.OnWaveStarted += OnWaveStarted;
                x.OnWaveFinished += OnWaveFinished;
            });
        }

        private void OnWaveFinished(int arg1, Enemies.Waves.IWave arg2)
        {
            Image.color = ReadyColor;
            Button.interactable = true;
        }

        private void OnWaveStarted(int arg1, Enemies.Waves.IWave arg2)
        {
            Image.color = InProgressColor;
            Button.interactable = false;
        }

        private void OnWavePreparing(int obj)
        {
            Image.color = PreparingColor;
            Button.interactable = false;
        }
    }
}
