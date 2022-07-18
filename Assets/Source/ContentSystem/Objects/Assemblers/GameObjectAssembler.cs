using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Assemblers
{
    public class GameObjectAssembler
    {
        private ComponentAssembler _componentAssembler = new ComponentAssembler();

        public GameObject Assemble(RootModel model)
        {
            AssemblyContext context = new AssemblyContext();
            var gameObject = Assemble(model.Root as ObjectModel, context);
            context.ReturnReferenceRequests();
            return gameObject;
        }

        public GameObject Assemble(ObjectModel model, AssemblyContext context)
        {
            GameObject obj = RecursiveAssemble(model, context, null);
            ReflectionUtils.DynamicBroadcastInvoke(obj, "OnAssembled", true);
            ReflectionUtils.DynamicBroadcastInvoke(obj, "OnPostAssembled", true);
            return obj;
        }

        private GameObject RecursiveAssemble (ObjectModel model, AssemblyContext context, Transform parent)
        {
            GameObject obj = new GameObject(model.GetValue<string>("Name"))
            {
                tag = model.GetValue<string>("Tag"),
                layer = model.GetValue<int>("Layer"),
                isStatic = model.GetValue<bool>("Static")
            };

            Vector3 pos = obj.transform.position;
            Quaternion rot = obj.transform.rotation;
            Vector3 scale = obj.transform.localScale;

            obj.transform.SetParent(parent, true);

            obj.transform.localPosition = pos;
            obj.transform.localRotation = rot;
            obj.transform.localScale = scale;

            obj.SetActive(false);
            var components = model.GetArray("Components");
            foreach (var component in components)
            {
                _componentAssembler.Assemble(component as ObjectModel, obj, context);
            }

            var children = model.GetArray("Children");
            foreach (var child in children)
            {
                GameObject childObj = RecursiveAssemble(child as ObjectModel, context, obj.transform);
            }
            obj.SetActive(true);

            return context.MakeReferencable(obj, model.Guid);
        }

        public RootModel Disassemble(GameObject gameObject)
        {
            DisassemblyContext context = new DisassemblyContext();
            RootModel model = new RootModel (Disassemble(gameObject, context));
            context.ReturnGuidRequests();
            return model;
        }

        public ObjectModel Disassemble(GameObject gameObject, DisassemblyContext context)
        {
            GameObject copy = Object.Instantiate(gameObject);
            ObjectModel model = RecursiveDisassemble(copy, context);
            Object.DestroyImmediate(copy);
            return model;
        }

        public ObjectModel RecursiveDisassemble (GameObject gameObject, DisassemblyContext context)
        {
            var children = new List<ObjectModel>();
            Component[] components = gameObject.GetComponents<Component>().Where(x => !x.GetType().IsDefined(typeof(DontSerializeAttribute), false)).ToArray();
            var componentModels = new List<ObjectModel>();
            foreach (Component component in components)
            {
                componentModels.Add(_componentAssembler.Disassemble(component, context));
            }

            return context.MakeReferencable (gameObject, new ObjectModel(
                new ObjectField("Name", ValueModelFactory.Create(gameObject.name, context)),
                new ObjectField("Tag", ValueModelFactory.Create(gameObject.tag, context)),
                new ObjectField("Layer", ValueModelFactory.Create(gameObject.layer, context)),
                new ObjectField("Static", ValueModelFactory.Create(gameObject.isStatic, context)),
                new ObjectField("Components", new ArrayModel(componentModels)),
                new ObjectField("Children", new ArrayModel(GetChildren(gameObject).Select(x => RecursiveDisassemble(x, context))))
            ));
        }

        private IEnumerable<GameObject> GetChildren (GameObject go)
        {
            foreach (Transform child in go.transform)
            {
                yield return child.gameObject;
            }
        }
    }
}