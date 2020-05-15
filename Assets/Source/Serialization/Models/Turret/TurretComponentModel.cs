using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.EngineObjectSerializers;
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

        public JToken Serialize()
        {
            Vector3Serializer serializer = new Vector3Serializer();

            return new JObject()
            {
                {"ComponentIdentifier", ComponentIdentifier },
                {"RelativePosition", serializer.Serialize(RelativePosition) },
                {"Children", new JArray(GetChildren().Select(x => x.Serialize()).ToArray()) }
            };
        }

        public void Deserialize(JToken source)
        {
            Vector3Serializer serializer = new Vector3Serializer();
            ComponentIdentifier = source["ComponentIdentifier"].ToString();
            RelativePosition = serializer.DeserializeValue(source["RelativePosition"]);

            List<ITurretComponentModel> children = new List<ITurretComponentModel>();
            foreach (JToken token in source["Children"] as JArray)
            {
                TurretComponentModel child = new TurretComponentModel();
                child.Deserialize(token);
                children.Add(child);
            }
            _children = children.ToArray();
        }
    }
}
