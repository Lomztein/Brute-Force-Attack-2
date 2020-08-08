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
        public override void Deserialize(IComponentModel model, Rigidbody2D target)
        {
            var properties = model.GetProperties();
            target.bodyType = properties.GetProperty("BodyType").Value.ToObject<RigidbodyType2D>();
        }

        public override IComponentModel Serialize(Rigidbody2D source)
        {
            return new ComponentModel(typeof(Rigidbody2D),
                new PropertyModel("BodyType", new JValue(source.bodyType))
                );
        }
    }
}
