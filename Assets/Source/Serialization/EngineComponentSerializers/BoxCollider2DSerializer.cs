using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.EngineObjectSerializers;
using Lomztein.BFA2.Serialization.Models.Component;
using Lomztein.BFA2.Serialization.Models.Property;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public class BoxCollider2DSerializer : EngineComponentSerializerBase<BoxCollider2D>
    {
        public override void Deserialize(IComponentModel model, BoxCollider2D target)
        {
            Vector2Serializer serializer = new Vector2Serializer();
            var properties = model.GetProperties();

            target.offset = serializer.DeserializeValue(properties.GetProperty("Offset").Value);
            target.size = serializer.DeserializeValue(properties.GetProperty("Size").Value);
        }

        public override IComponentModel Serialize(BoxCollider2D source)
        {
            Vector2Serializer serializer = new Vector2Serializer();

            return new ComponentModel(typeof(BoxCollider2D),
                new PropertyModel("Offset", serializer.Serialize(source.offset)),
                new PropertyModel("Size", serializer.Serialize(source.size))
                );
        }
    }
}
