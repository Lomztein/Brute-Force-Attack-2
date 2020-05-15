using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Content.References.GameObjects;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Serialization.Models.Turret;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.Turret
{
    public class GameObjectTurretComponentAssembler
    {
        private static string COMPONENTS_CONTENT_PATH = "*/Components";
        private static CachedGameObject[] _allComponents;

        public void Assemble (ITurretComponentModel model, ITurretComponent parent, ITurretAssembly assembly)
        {
            CachedGameObject component = GetComponent(model.ComponentIdentifier);
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
            assembly.AddComponent(newComponent);

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

        private CachedGameObject[] GetComponents ()
        {
            if (_allComponents == null)
            {
                List<CachedGameObject> objects = new List<CachedGameObject>();
                var components = Content.Content.GetAll(COMPONENTS_CONTENT_PATH, typeof(IGameObjectModel));
                foreach (IGameObjectModel component in components)
                {
                    objects.Add(new CachedGameObject(new ContentGameObjectModel(component)));
                }
                _allComponents = objects.ToArray();
            }
            return _allComponents;
        }

        private CachedGameObject GetComponent (string identifier)
        {
            return GetComponents().First(x => x.Get().GetComponent<ITurretComponent>().UniqueIdentifier == identifier);
        }
    }
}
