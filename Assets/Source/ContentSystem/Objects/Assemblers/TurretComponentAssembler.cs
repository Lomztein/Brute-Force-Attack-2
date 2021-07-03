using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Turrets;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.ContentSystem.Assemblers
{
    public class TurretComponentAssembler
    {
        private static readonly string COMPONENTS_CONTENT_PATH = "*/Components";
        private IContentCachedPrefab[] _allComponents;

        public TurretComponent Assemble (ObjectModel model, Transform parent, TurretAssembly assembly, AssemblyContext context)
        {
            IContentCachedPrefab component = GetComponent(model.GetValue<string>("UniqueIdentifier"));
            ValueAssembler assembler = new ValueAssembler();
            GameObject obj = component.Instantiate();

            if (parent != null)
            {
                obj.transform.SetParent(parent.transform);
                obj.transform.localPosition = (Vector3)assembler.Assemble (model.GetObject("LocalPosition"), typeof (Vector3), context);
            }
            else
            {
                obj.transform.SetParent(assembly.transform);
                obj.transform.localPosition = Vector3.zero;
            }

            TurretComponent newComponent = obj.GetComponent<TurretComponent>();
            foreach (ValueModel child in model.GetArray("Children"))
            {
                Assemble(child as ObjectModel, newComponent.transform, assembly, context);
            }

            return newComponent;
        }

        public ObjectModel Dissassemble (TurretComponent component, DisassemblyContext context)
        {
            GameObject obj = component.gameObject;
            List<ObjectModel> children = new List<ObjectModel>();
            foreach (Transform child in obj.transform)
            {
                TurretComponent childComponent = child.GetComponent<TurretComponent>();
                if (childComponent != null)
                {
                    children.Add(Dissassemble(childComponent, context));
                }
            }

            return new ObjectModel(
                new ObjectField("UniqueIdentifier", ValueModelFactory.Create(component.UniqueIdentifier, context)),
                new ObjectField("LocalPosition", ValueModelFactory.Create(obj.transform.localPosition, context)),
                new ObjectField("Children", new ArrayModel(children))
                );
        }

        public IContentCachedPrefab[] GetComponents ()
        {
            if (_allComponents == null)
            {
                _allComponents = Content.GetAll(COMPONENTS_CONTENT_PATH, typeof (IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
            }
            return _allComponents;
        }

        public IContentCachedPrefab GetComponent (string identifier)
        {
            return GetComponents().First(x => x.GetCache().GetComponent<TurretComponent>().UniqueIdentifier == identifier);
        }
    }
}
