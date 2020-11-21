﻿using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Turrets;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Content.Assemblers
{
    public class TurretComponentAssembler
    {
        private static readonly string COMPONENTS_CONTENT_PATH = "*/Components";
        private static IContentCachedPrefab[] _allComponents;

        public void Assemble (ObjectModel model, TurretComponent parent, TurretAssembly assembly)
        {
            IContentCachedPrefab component = GetComponent(model.GetValue<string>("UniqueIdentifier"));
            ObjectAssembler assembler = new ObjectAssembler();
            GameObject obj = component.Instantiate();

            if (parent != null)
            {
                obj.transform.SetParent((parent as Component).transform);
                obj.transform.localPosition = (Vector3)assembler.Assemble (model.GetObject("LocalPosition"), typeof (Vector3));
            }
            else
            {
                obj.transform.SetParent((assembly as Component).transform);
                obj.transform.localPosition = Vector3.zero;
            }

            TurretComponent newComponent = obj.GetComponent<TurretComponent>();
            foreach (ValueModel child in model.GetArray("Children"))
            {
                Assemble(child as ObjectModel, newComponent, assembly);
            }
        }

        public ObjectModel Dissassemble (TurretComponent component)
        {
            GameObject obj = (component as Component).gameObject;
            List<ObjectModel> children = new List<ObjectModel>();
            foreach (Transform child in obj.transform)
            {
                TurretComponent childComponent = child.GetComponent<TurretComponent>();
                if (childComponent != null)
                {
                    children.Add(Dissassemble(childComponent));
                }
            }

            return new ObjectModel(
                new ObjectField("UniqueIdentifier", ValueModelFactory.Create(component.UniqueIdentifier)),
                new ObjectField("LocalPosition", ValueModelFactory.Create(obj.transform.localPosition)),
                new ObjectField("Children", new ArrayModel(children))
                );
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
            return GetComponents().First(x => x.GetCache().GetComponent<TurretComponent>().UniqueIdentifier == identifier);
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
