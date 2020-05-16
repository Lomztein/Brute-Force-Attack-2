using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Component;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public class Rigidbody2DSerializer : EngineComponentSerializerBase<Rigidbody2D>
    {
        public override void Deserialize(IComponentModel model, GameObject target)
        {
            Rigidbody2D body = target.AddComponent<Rigidbody2D>();
            var properties = model.GetProperties();
            body.bodyType = properties.GetProperty("BodyType").Value.ToObject<RigidbodyType2D>();
        }

        public override IComponentModel Serialize(Rigidbody2D source)
        {
            return new ComponentModel(typeof(Rigidbody2D),
                new PropertyModel("BodyType", new JValue(source.bodyType))
                );
        }
    }
}
