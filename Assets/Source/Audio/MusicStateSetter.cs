using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Audio
{
    public class MusicStateSetter : MonoBehaviour
    {
        public MusicState State;
        public bool SetOnAwake;

        private void Awake()
        {
            if (SetOnAwake)
            {
                SetState();
            }
        }

        public void SetState()
        {
            MusicManager.SetMusicState(State, true);
        }
    }
}
