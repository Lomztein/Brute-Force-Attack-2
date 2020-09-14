using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class GameObjectAssembler : IGameObjectAssembler
    {
        private ComponentAssembler _componentAssembler = new ComponentAssembler();

        public GameObject Assemble(IGameObjectModel model)
        {
            GameObject obj = RecursiveAssemble(model);
            ReflectionUtils.DynamicBroadcastInvoke(obj, "OnAssembled");
            return obj;
        }

        private GameObject RecursiveAssemble (IGameObjectModel model)
        {
            GameObject obj = new GameObject(model.Name)
            {
                tag = model.Tag,
                layer = model.Layer,
                isStatic = model.Static
            };

            var components = model.GetComponentModels();
            foreach (var component in components)
            {
                _componentAssembler.Assemble(component, obj);
            }

            var children = model.GetChildren();
            foreach (var child in children)
            {
                GameObject childObj = RecursiveAssemble(child);

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

        public IGameObjectModel Disassemble(GameObject gameObject)
        {
            var children = new List<IGameObjectModel>();
            foreach (Transform child in gameObject.transform)
            {
                children.Add(Disassemble(child.gameObject));
            }

            Component[] components = gameObject.GetComponents<Component>().Where(x => !x.GetType().IsDefined(typeof (DontSerializeAttribute), false)).ToArray();
            var componentModels = new List<IObjectModel>();
            foreach (Component component in components)
            {
                componentModels.Add(_componentAssembler.Dissasemble(component));
            }

            return new GameObjectModel(gameObject.name, gameObject.tag, gameObject.layer, gameObject.isStatic, children, componentModels);
        }
    }
}