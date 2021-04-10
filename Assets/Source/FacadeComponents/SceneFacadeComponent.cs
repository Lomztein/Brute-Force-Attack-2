using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.FacadeComponents
{
    public abstract class SceneFacadeComponent : FacadeComponent
    {
        protected abstract int SceneBuildIndex { get; }

        public override bool Active => _active;
        private bool _active;

        public event Action OnAttached;
        public event Action OnDetached;

        public override void Init(Facade facade)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Detach();
            _active = false;
            OnDetached?.Invoke();
            if (arg0.buildIndex == SceneBuildIndex)
            {
                Attach(arg0);
                _active = true;
                OnAttached?.Invoke();
            }
        }

        public abstract void Attach(Scene scene);

        public abstract void Detach();
    }
}
