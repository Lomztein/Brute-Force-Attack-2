using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.FacadeComponents
{
    public abstract class SceneFacadeComponent : IFacadeComponent
    {
        protected abstract int SceneBuildIndex { get; }

        public bool Active => _active;
        private bool _active;

        public event Action OnSceneLoaded;
        public event Action OnSceneUnloaded;

        public void Init()
        {
            SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
        }

        private void SceneManager_OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Detach();
            _active = false;
            OnSceneUnloaded?.Invoke();
            if (arg0.buildIndex == SceneBuildIndex)
            {
                Attach(arg0);
                _active = true;
                OnSceneLoaded?.Invoke();
            }
        }

        public abstract void Attach(Scene scene);

        public abstract void Detach();
    }
}
