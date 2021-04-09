using Lomztein.BFA2.FacadeComponents;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class Facade
    {
        private static Facade _instance;

        private List<FacadeComponent> _components = new List<FacadeComponent>();

        public MainMenuFacade MainMenu => GetComponent<MainMenuFacade>();
        public BattlefieldFacade Battlefield => GetComponent<BattlefieldFacade>();

        public static Facade GetInstance ()
        {
            if (_instance == null)
            {
                _instance = new Facade();
                _instance.Init();
            }
            return _instance;
        }

        public T GetComponent<T>() where T : FacadeComponent
        {
            foreach (FacadeComponent component in _components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            throw new InvalidOperationException("The requested FacadeComponent was not found, please ensure that the FacadeComponent is added to the Facade during preloading.");
        }

        public void AddFacadeComponent (FacadeComponent component)
        {
            _components.Add(component);
        }

        internal void Init ()
        {
            _components.AddRange(ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<FacadeComponent>());

            foreach (FacadeComponent component in _components)
            {
                component.Init(this);
            }
        }
    }
}
