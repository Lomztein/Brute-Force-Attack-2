using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Content.Assemblers
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
                _componentAssembler.Assemble((component as ComplexPropertyModel).Model, obj);
            }

            var children = model.GetArray("Children");
            foreach (var child in children)
            {
                GameObject childObj = RecursiveAssemble((child as ComplexPropertyModel).Model);

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
                componentModels.Add(_componentAssembler.Disassemble(component));
            }

            return new ObjectModel(typeof(ObjectModel),
                new ObjectField("Name", PropertyModelFactory.Create(gameObject.name)),
                new ObjectField("Tag", PropertyModelFactory.Create(gameObject.tag)),
                new ObjectField("Layer", PropertyModelFactory.Create(gameObject.layer)),
                new ObjectField("Static", PropertyModelFactory.Create(gameObject.isStatic)),
                new ObjectField("Components", new ArrayPropertyModel(null, componentModels.Select(x => new ComplexPropertyModel(x).MakeExplicit()))),
                new ObjectField("Children", new ArrayPropertyModel(null, GetChildren(gameObject).Select(x => new ComplexPropertyModel(Disassemble(x)))))
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