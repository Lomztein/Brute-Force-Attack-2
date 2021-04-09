using Lomztein.BFA2.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.FacadeComponents
{
    public class MainMenuFacade : FacadeComponent
    {
        public override bool Active => _navigation != null;
        private const int _mainMenuBuildIndex = 0;

        private MenuNavigation _navigation;

        public event Action<MenuWindow, MenuWindow> OnWindowChanged;

        public override void Init(Facade facade)
        {
            facade.GetComponent<SceneManagerFacade>().OnSceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (_navigation != null)
            {
                Detach();
            }
            if (arg0.buildIndex == _mainMenuBuildIndex)
            {
                Attach();
            }
        }

        private void Attach ()
        {
            _navigation = MenuNavigation.Instance;
            _navigation.OnWindowChanged += WindowChanged;
        }

        private void Detach ()
        {
            _navigation.OnWindowChanged -= WindowChanged;
            _navigation = null;
        }

        private void WindowChanged(MenuWindow prev, MenuWindow next)
        {
            OnWindowChanged?.Invoke(prev, next);
        }
    }
}
