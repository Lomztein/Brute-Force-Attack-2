using Lomztein.BFA2.Serialization.Models.Turret;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers.Turret
{
    public class TurretAssemblyModelSerializer : ISerializer<ITurretAssemblyModel>
    {
        private TurretComponentModelSerializer _componentSerializer;

        public ITurretAssemblyModel Deserialize(JToken value)
        {
            return new TurretAssemblyModel(
                value["Name"].ToString(),
                value["Description"].ToString(),
                _componentSerializer.Deserialize(value["RootComponent"])
                );
        }

        public JToken Serialize(ITurretAssemblyModel value)
        {
            return new JObject()
            {
                {"Name", value.Name },
                {"Description", value.Description },
                {"RootComponent", _componentSerializer.Serialize(value.RootComponent) }
            };
        }
    }
}
