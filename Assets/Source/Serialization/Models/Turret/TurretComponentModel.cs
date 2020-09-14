using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models.Turret
{
    public class TurretComponentModel : ITurretComponentModel
    {
        public string ComponentIdentifier { get; private set; }
        public Vector3 RelativePosition { get; private set; }

        private ITurretComponentModel[] _children;
        public ITurretComponentModel[] GetChildren() => _children.ToArray();

        public TurretComponentModel() { }

        public TurretComponentModel(string identifier, Vector3 relativePosition, ITurretComponentModel[] children)
        {
            ComponentIdentifier = identifier;
            RelativePosition = relativePosition;
            _children = children;
        }
    }
}
