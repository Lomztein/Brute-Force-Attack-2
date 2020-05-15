using Lomztein.BFA2.Serialization.Models.Component;
using Lomztein.BFA2.Serialization.Models.GameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class GameObjectAssembler : IGameObjectAssembler
    {
        private ComponentAssembler _componentAssembler = new ComponentAssembler();

        public GameObject Assemble(IGameObjectModel model)
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
                GameObject childObj = Assemble(child);
                childObj.transform.SetParent(obj.transform);
            }

            obj.BroadcastMessage("OnGameObjectAssembled", SendMessageOptions.DontRequireReceiver);
            return obj;
        }

        public IGameObjectModel Disassemble(GameObject gameObject)
        {
            var children = new List<IGameObjectModel>();
            foreach (Transform child in gameObject.transform)
            {
                children.Add(Disassemble(child.gameObject));
            }

            Component[] components = gameObject.GetComponents<Component>();
            var componentModels = new List<IComponentModel>();
            foreach (Component component in components)
            {
                componentModels.Add(_componentAssembler.Dissasemble(component));
            }

            return new GameObjectModel(gameObject.name, gameObject.tag, gameObject.layer, gameObject.isStatic, children, componentModels);
        }
    }
}