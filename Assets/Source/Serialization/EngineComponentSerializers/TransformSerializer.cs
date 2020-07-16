using Lomztein.BFA2.Serialization.EngineObjectSerializers;
using Lomztein.BFA2.Serialization.Models.Component;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public class TransformSerializer : EngineComponentSerializerBase<Transform>
    {
        public override void Deserialize(IComponentModel model, GameObject target)
        {
            var properties = model.GetProperties();
            Vector3Serializer serializer = new Vector3Serializer();

            Vector3 position = serializer.DeserializeValue(properties.GetProperty("Position").Value);
            Vector3 rotation = serializer.DeserializeValue(properties.GetProperty("Rotation").Value);
            Vector3 scale = serializer.DeserializeValue(properties.GetProperty("Scale").Value);

            target.transform.localPosition = position;
            target.transform.localRotation = Quaternion.Euler (rotation);
            target.transform.localScale = scale;
        }

        public override IComponentModel Serialize(Transform source)
        {
            Vector3Serializer serializer = new Vector3Serializer();

            return new ComponentModel(typeof(Transform),
                new PropertyModel("Position", serializer.Serialize(source.localPosition)),
                new PropertyModel("Rotation", serializer.Serialize(source.localRotation.eulerAngles)),
                new PropertyModel("Scale", serializer.Serialize(source.localScale))
                );
        }
    }
}
