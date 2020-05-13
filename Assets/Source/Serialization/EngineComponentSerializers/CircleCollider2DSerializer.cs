using Lomztein.BFA2.Serialization.EngineObjectSerializers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public class CircleCollider2DSerializer : EngineComponentSerializerBase<CircleCollider2D>
    {
        public override void Deserialize(IComponentModel model, GameObject target)
        {
            IPropertyModel[] properties = model.GetProperties();
            CircleCollider2D collider = target.AddComponent<CircleCollider2D>();
            Vector2Serializer vs = new Vector2Serializer();

            collider.radius = properties.GetProperty("Radius").Value.ToObject<float>();
            collider.offset = vs.DeserializeValue(properties.GetProperty("Offset").Value);
            collider.isTrigger = properties.GetProperty("IsTrigger").Value.ToObject<bool>();
            collider.usedByEffector = properties.GetProperty("UsedByEffector").Value.ToObject<bool>();
        }

        public override IComponentModel Serialize(CircleCollider2D source)
        {
            Vector2Serializer vs = new Vector2Serializer();

            return new ComponentModel(
                source.GetType(),
                new PropertyModel("Radius", new JValue(source.radius)),
                new PropertyModel("Offset", vs.Serialize(source.offset)),
                new PropertyModel("IsTrigger", new JValue(source.isTrigger)),
                new PropertyModel("UsedByEffector", new JValue(source.usedByEffector)));
        }
    }
}
