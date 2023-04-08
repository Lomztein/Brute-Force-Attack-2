using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.UI.Messages;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield
{
    public class BattlefieldAutosaver : MonoBehaviour
    {
        public string AutosaveFileName = "Autosave";

        // Start is called before the first frame update
        void Start()
        {
            RoundController.Instance.OnWaveFinished += Instance_OnWaveFinished;
        }

        private void Instance_OnWaveFinished(int arg1, Enemies.Waves.WaveHandler arg2)
        {
            if (RoundController.Instance.ActiveWaves.Length == 0)
            {
                BattlefieldSave.SaveCurrentToFile(Path.Combine(BattlefieldSave.PATH_ROOT, AutosaveFileName + ".json"));
                Message.Send("Autosave complete.", Message.Type.Minor);
            }
        }
    }
}
