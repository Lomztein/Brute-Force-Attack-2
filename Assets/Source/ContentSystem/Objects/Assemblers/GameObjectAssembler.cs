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
            GameObject obj = RecursiveAssemble(model.Root as ObjectModel, new AssemblyContext());
            ReflectionUtils.DynamicBroadcastInvoke(obj, "OnAssembled");
            ReflectionUtils.DynamicBroadcastInvoke(obj, "OnPostAssembled");
            return obj;
        }

        private GameObject RecursiveAssemble (ObjectModel model, AssemblyContext context)
        {
            GameObject obj = new GameObject(model.GetValue<string>("Name"))
            {
                tag = model.GetValue<string>("Tag"),
                layer = model.GetValue<int>("Layer"),
                isStatic = model.GetValue<bool>("Static")
            };

            var components = model.GetArray("Components");
            foreach (var component in components)
            {
                _componentAssembler.Assemble(component as ObjectModel, obj, context);
            }

            var children = model.GetArray("Children");
            foreach (var child in children)
            {
                GameObject childObj = RecursiveAssemble(child as ObjectModel, context);

                Vector3 pos = childObj.transform.position;
                Quaternion rot = childObj.transform.rotation;
                Vector3 scale = childObj.transform.localScale;

                childObj.transform.SetParent(obj.transform);

                childObj.transform.localPosition = pos;
                childObj.transform.localRotation = rot;
                childObj.transform.localScale = scale;
            }

            return obj;
        }

        public RootModel Disassemble(GameObject gameObject)
        {
            DisassemblyContext context = new DisassemblyContext();
            RootModel model = new RootModel (RecursiveDisassemble(gameObject, context));
            context.ReturnGuidRequests();
            return model;
        }

        public ObjectModel RecursiveDisassemble (GameObject gameObject, DisassemblyContext context)
        {
            var children = new List<ObjectModel>();
            Component[] components = gameObject.GetComponents<Component>().Where(x => !x.GetType().IsDefined(typeof(DontSerializeAttribute), false)).ToArray();
            var componentModels = new List<ObjectModel>();
            foreach (Component component in components)
            {
                componentModels.Add(_componentAssembler.Disassemble(component, context).MakeExplicit(component.GetType()) as ObjectModel);
            }

            return context.MakeReferencable (gameObject, new ObjectModel(
                new ObjectField("Name", ValueModelFactory.Create(gameObject.name, context)),
                new ObjectField("Tag", ValueModelFactory.Create(gameObject.tag, context)),
                new ObjectField("Layer", ValueModelFactory.Create(gameObject.layer, context)),
                new ObjectField("Static", ValueModelFactory.Create(gameObject.isStatic, context)),
                new ObjectField("Components", new ArrayModel(componentModels)),
                new ObjectField("Children", new ArrayModel(GetChildren(gameObject).Select(x => RecursiveDisassemble(x, context))))
            )) as ObjectModel;
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