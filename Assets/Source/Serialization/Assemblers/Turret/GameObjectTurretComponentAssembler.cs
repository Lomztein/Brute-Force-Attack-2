using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Serialization.Models.Turret;
using Lomztein.BFA2.Turrets;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Serialization.Assemblers.Turret
{
    public class GameObjectTurretComponentAssembler
    {
        private static string COMPONENTS_CONTENT_PATH = "*/Components";
        private static IContentCachedPrefab[] _allComponents;

        public void Assemble (ITurretComponentModel model, ITurretComponent parent, ITurretAssembly assembly)
        {
            IContentCachedPrefab component = GetComponent(model.ComponentIdentifier);
            GameObject obj = component.Instantiate();

            if (parent != null)
            {
                obj.transform.SetParent((parent as Component).transform);
                obj.transform.localPosition = model.RelativePosition;
            }
            else
            {
                obj.transform.SetParent((assembly as Component).transform);
                obj.transform.localPosition = Vector3.zero;
            }

            ITurretComponent newComponent = obj.GetComponent<ITurretComponent>();
            foreach (ITurretComponentModel child in model.GetChildren())
            {
                Assemble(child, newComponent, assembly);
            }
        }

        public ITurretComponentModel Dissassemble (ITurretComponent component)
        {
            GameObject obj = (component as Component).gameObject;
            List<ITurretComponentModel> children = new List<ITurretComponentModel>();
            foreach (Transform child in obj.transform)
            {
                ITurretComponent childComponent = child.GetComponent<ITurretComponent>();
                if (childComponent != null)
                {
                    children.Add(Dissassemble(childComponent));
                }
            }

            return new TurretComponentModel(component.UniqueIdentifier, obj.transform.localPosition, children.ToArray());
        }

        public static IContentCachedPrefab[] GetComponents ()
        {
            if (_allComponents == null)
            {
                _allComponents = ContentSystem.Content.GetAll(COMPONENTS_CONTENT_PATH, typeof (IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
                SceneManager.sceneUnloaded += SceneUnloaded; // Cleanup must be handled manually, as this is not a MonoBehaviour and thus will not be cleaned automnatically.
            }
            return _allComponents;
        }

        private static void SceneUnloaded(Scene arg0)
        {
            DisposeComponents();   
        }

        public static IContentCachedPrefab GetComponent (string identifier)
        {
            return GetComponents().First(x => x.GetCache().GetComponent<ITurretComponent>().UniqueIdentifier == identifier);
        }
        
        private static void DisposeComponents ()
        {
            foreach (var component in GetComponents())
            {
                component.Dispose();
            }
            _allComponents = null;
        }
    }
}
