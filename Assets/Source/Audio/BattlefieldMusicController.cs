using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Audio
{
    public class BattlefieldMusicController : MonoBehaviour
    {
        public MusicState PeaceState;
        public MusicState CombatState;

        private void Start()
        {
            RoundController.Instance.OnWaveStarted += Instance_OnWaveStarted;
            RoundController.Instance.OnWaveFinished += Instance_OnWaveFinished;
        }

        private void OnDestroy()
        {
            RoundController.Instance.OnWaveStarted -= Instance_OnWaveStarted;
            RoundController.Instance.OnWaveFinished -= Instance_OnWaveFinished;
        }

        private void Instance_OnWaveStarted(int arg1, WaveHandler arg2)
        {
            if (MusicManager.CurrentState != CombatState)
            {
                MusicManager.SetMusicState(CombatState, true);
            }
        }

        private void Instance_OnWaveFinished(int arg1, WaveHandler arg2)
        {
            if (RoundController.Instance.ActiveWaves.Length == 0)
            {
                MusicManager.SetMusicState(PeaceState, true);
            }
        }
    }
}
