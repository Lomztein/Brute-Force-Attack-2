using Lomztein.BFA2.Serialization;
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

        public GameObject Assemble(ObjectModel model)
        {
            GameObject obj = RecursiveAssemble(model);
            ReflectionUtils.DynamicBroadcastInvoke(obj, "OnAssembled");
            return obj;
        }

        private GameObject RecursiveAssemble (ObjectModel model)
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
                _componentAssembler.Assemble(component as ObjectModel, obj);
            }

            var children = model.GetArray("Children");
            foreach (var child in children)
            {
                GameObject childObj = RecursiveAssemble(child as ObjectModel);

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

        public ObjectModel Disassemble(GameObject gameObject)
        {
            var children = new List<ObjectModel>();
            foreach (Transform child in gameObject.transform)
            {
                children.Add(Disassemble(child.gameObject));
            }

            Component[] components = gameObject.GetComponents<Component>().Where(x => !x.GetType().IsDefined(typeof (DontSerializeAttribute), false)).ToArray();
            var componentModels = new List<ObjectModel>();
            foreach (Component component in components)
            {
                componentModels.Add(_componentAssembler.Disassemble(component).MakeExplicit(component.GetType()) as ObjectModel);
            }

            return new ObjectModel(
                new ObjectField("Name", ValueModelFactory.Create(gameObject.name)),
                new ObjectField("Tag", ValueModelFactory.Create(gameObject.tag)),
                new ObjectField("Layer", ValueModelFactory.Create(gameObject.layer)),
                new ObjectField("Static", ValueModelFactory.Create(gameObject.isStatic)),
                new ObjectField("Components", new ArrayModel(componentModels)),
                new ObjectField("Children", new ArrayModel(GetChildren(gameObject).Select(x => Disassemble(x))))
            );
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