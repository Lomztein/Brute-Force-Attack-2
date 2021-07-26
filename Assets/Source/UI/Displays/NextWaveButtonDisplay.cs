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
                x.OnStateChanged += OnStateChanged;
            });
        }

        private void OnStateChanged(RoundController.RoundState state)
        {
            switch (state)
            {
                case RoundController.RoundState.Ready:
                    Image.color = ReadyColor;
                    Button.interactable = true;
                    break;

                case RoundController.RoundState.Preparing:
                    Image.color = PreparingColor;
                    Button.interactable = false;
                    break;

                case RoundController.RoundState.InProgress:
                    Image.color = InProgressColor;
                    Button.interactable = true;
                    break;

            }

        }
    }
}
