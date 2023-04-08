using Lomztein.BFA2.Scenes.Battlefield;
using Lomztein.BFA2.Serialization.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.MainMenu
{
    public class LoadGame : MonoBehaviour
    {
        public void Load()
        {
            FileBrowser.Create("Select Save File", BattlefieldSave.PATH_ROOT, ".json", OnLoad);
        }

        private void OnLoad(string path)
        {
            BattlefieldInitializeInfo.InitType = BattlefieldInitializeInfo.InitializeType.Load;
            BattlefieldInitializeInfo.LoadFileName = path;
            SceneManager.LoadScene("Battlefield");
        }
    }
}
