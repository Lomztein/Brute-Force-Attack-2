using Lomztein.BFA2.FacadeComponents;
using Lomztein.BFA2.FacadeComponents.Battlefield;
using Lomztein.BFA2.FacadeComponents.MainMenu;
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

        private List<IFacadeComponent> _components = new List<IFacadeComponent>();

        public static MainMenuFacade MainMenu => GetComponent<MainMenuFacade>();
        public static BattlefieldFacade Battlefield => GetComponent<BattlefieldFacade>();

        private static Facade GetInstance ()
        {
            if (_instance == null)
            {
                _instance = new Facade();
                _instance.InitComponents();
            }
            return _instance;
        }

        public static void Init ()
        {
            GetInstance();
        }

        public static T GetComponent<T>() where T : IFacadeComponent
        {
            foreach (IFacadeComponent component in GetInstance()._components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            throw new InvalidOperationException("The requested FacadeComponent was not found, please ensure that the FacadeComponent is added to the Facade during preloading.");
        }

        public static void AddFacadeComponent (IFacadeComponent component)
        {
            GetInstance()._components.Add(component);
        }

        internal void InitComponents ()
        {
            _components.AddRange(ReflectionUtils.InstantiateAllOfTypeFromGameAssemblies<IFacadeComponent>());

            foreach (IFacadeComponent component in _components)
            {
                component.Init();
            }
        }
    }
}
