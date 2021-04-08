using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.MainMenu
{
    public class SceneLoader : MonoBehaviour
    {
        public string SceneName;

        public void LoadScene ()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
