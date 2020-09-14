using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Component;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models.GameObject
{
    public class GameObjectModel : IGameObjectModel
    {
        public string Name { get; private set; }
        public string Tag { get; private set; }
        public int Layer { get; private set; }
        public bool Static { get; private set; }

        private List<IGameObjectModel> _children = new List<IGameObjectModel>();
        private List<IObjectModel> _componentModels = new List<IObjectModel>();

        public GameObjectModel() { }

        public GameObjectModel(string name, string tag, int layer, bool @static, IEnumerable<IGameObjectModel> children, IEnumerable<IObjectModel> componentModels)
        {
            Name = name;
            Tag = tag;
            Layer = layer;
            Static = @static;
            _children = children.ToList();
            _componentModels = componentModels.ToList();
        }

        public IObjectModel[] GetComponentModels() => _componentModels.ToArray();
        public IGameObjectModel[] GetChildren() => _children.ToArray();

        public override string ToString()
        {
            return Name;
        }
    }
}
