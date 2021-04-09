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
    public class MainMenuFacade : SceneFacadeComponent
    {
        private MenuNavigation _navigation;
        protected override int SceneBuildIndex => 0;

        public event Action<MenuWindow, MenuWindow> OnWindowChanged;

        public override void Attach (Scene scene)
        {
            _navigation = MenuNavigation.Instance;
            _navigation.OnWindowChanged += WindowChanged;
        }

        public override void Detach ()
        {
            if (_navigation)
            {
                _navigation.OnWindowChanged -= WindowChanged;
                _navigation = null;
            }
        }

        private void WindowChanged(MenuWindow prev, MenuWindow next)
        {
            OnWindowChanged?.Invoke(prev, next);
        }
    }
}
