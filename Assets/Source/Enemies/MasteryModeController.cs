using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Scalers;
using Lomztein.BFA2.UI;
using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Enemies
{
    public class MasteryModeController : MonoBehaviour
    {
        public static MasteryModeController Instance;

        public RoundController RoundController;
        public EnemyScaler MasteryScaler = new EnemyScaler(1f, 1f, 1f);
        public GameObject MasteryWindow;

        public float ScalerCoeffecient;

        public int MasteryLevel;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            RoundController.OnWavesExhausted += RoundController_OnWavesExhausted;
            EnemyScaleController.Instance.AddEnemyScalers(MasteryScaler);
        }

        private void OnDestroy()
        {
            RoundController.OnWavesExhausted -= RoundController_OnWavesExhausted;
        }

        private void RoundController_OnWavesExhausted(int obj)
        {
            if (MasteryLevel == 0)
            {
                WindowManager.OpenWindowAboveOverlay(MasteryWindow);
            }
            else
            {
                IncrementMasteryMode();
            }
        }

        public void ReturnToMainMenu ()
        {
            SceneManager.LoadScene(0);
        }

        public void IncrementMasteryMode ()
        {
            MasteryLevel++;
            RoundController.SetWave(1);
            MasteryScaler.HealthMult *= Mathf.Pow(ScalerCoeffecient, RoundController.WaveCollection.GetWaveCount());
            Message.Send($"Mastery Mode Level {MasteryLevel}.\nPrepare to die.", Message.Type.Major);
        }
    }
}
