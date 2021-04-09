using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.FacadeComponents
{
    public class SceneManagerFacade : FacadeComponent
    {
        public override bool Active => true;

        public override void Init(Facade facade)
        {
            SceneManager.sceneLoaded += SceneLoaded;
            SceneManager.sceneUnloaded += SceneUnloaded;
        }

        private void SceneUnloaded(Scene arg0)
        {
            OnSceneUnloaded?.Invoke(arg0);
        }

        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            OnSceneLoaded?.Invoke(arg0, arg1);
        }

        public event Action<Scene, LoadSceneMode> OnSceneLoaded;
        public event Action<Scene> OnSceneUnloaded;
    }
}
