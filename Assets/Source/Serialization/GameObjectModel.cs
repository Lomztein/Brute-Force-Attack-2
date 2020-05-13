﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization
{
    class GameObjectModel : IGameObjectModel
    {
        public string Name { get; private set; }
        public string Tag { get; private set; }
        public int Layer { get; private set; }
        public bool Static { get; private set; }

        private List<IGameObjectModel> _children = new List<IGameObjectModel>();
        private List<IComponentModel> _componentModels = new List<IComponentModel>();

        public void Deserialize(JToken data)
        {
            Name = data["Name"].ToString ();
            Tag = data["Tag"].ToString();
            Layer = data["Layer"].ToObject<int>();
            Static = data["Static"].ToObject<bool>();

            JArray children = data["Children"] as JArray;
            foreach (JToken child in children)
            {
                IGameObjectModel model = new GameObjectModel();
                model.Deserialize(child);
                _children.Add(model);
            }

            JArray components = data["Components"] as JArray;
            foreach (JToken component in components)
            {
                ComponentModel model = new ComponentModel();
                model.Deserialize(component);
                _componentModels.Add(model);
            }
        }

        public IComponentModel[] GetComponentModels() => _componentModels.ToArray();

        public JToken Serialize()
        {
            return new JObject()
            {
                { "Name", new JValue(Name) },
                { "Tag", new JValue(Tag) },
                { "Layer", new JValue(Layer) },
                { "Static", new JValue(Static) },
                { "Children", new JArray (_children.Select (x => x.Serialize()).ToArray ()) },
                { "Components", new JArray (_componentModels.Select (x => x.Serialize()).ToArray ()) }
            };
        }

        public static GameObjectModel Create (GameObject baseObject)
        {
            GameObjectModel model = new GameObjectModel();
            model.Name = baseObject.name;
            model.Tag = baseObject.tag;
            model.Layer = baseObject.layer;
            model.Static = baseObject.isStatic;

            foreach (Transform child in baseObject.transform)
            {
                model._children.Add(GameObjectModel.Create(child.gameObject));
            }

            Component[] components = baseObject.GetComponents<Component>();
            foreach (Component component in components)
            {
                model._componentModels.Add(ComponentModel.Create(component));
            }

            return model;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
