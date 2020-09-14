using Lomztein.BFA2.Serialization.Assemblers.EngineObject;
using Lomztein.BFA2.Serialization.Models.Turret;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers.Turret
{
    public class TurretComponentModelSerializer : ISerializer<ITurretComponentModel>
    {
        public ITurretComponentModel Deserialize(JToken value)
        {
            Vector3Assembler assembler = new Vector3Assembler();
            ObjectModelSerializer serializer = new ObjectModelSerializer();

            var identifier = value["ComponentIdentifier"].ToString();
            var position = assembler.AssembleValue(serializer.Deserialize(value["RelativePosition"]));

            List<ITurretComponentModel> children = new List<ITurretComponentModel>();
            foreach (JToken token in value["Children"] as JArray)
            {
                ITurretComponentModel child = Deserialize(token);
                children.Add(child);
            }

            return new TurretComponentModel(identifier, position, children.ToArray());
        }

        public JToken Serialize(ITurretComponentModel value)
        {
            Vector3Assembler assembler = new Vector3Assembler();
            ObjectModelSerializer serializer = new ObjectModelSerializer();

            return new JObject()
            {
                {"ComponentIdentifier", value.ComponentIdentifier },
                {"RelativePosition", serializer.Serialize(assembler.DisassembleValue(value.RelativePosition)) },
                {"Children", new JArray(value.GetChildren().Select(x => Serialize(x)).ToArray()) }
            };
        }
    }
}
