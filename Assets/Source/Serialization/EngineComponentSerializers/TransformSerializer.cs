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

            Vector3 position = new Vector3(
                Convert.ToSingle (properties.GetProperty("PosX")),
                Convert.ToSingle(properties.GetProperty("PosY")),
                Convert.ToSingle(properties.GetProperty("PosZ"))
                );

            Vector3 rotation = new Vector3(
                Convert.ToSingle(properties.GetProperty("RotX")),
                Convert.ToSingle(properties.GetProperty("RotY")),
                Convert.ToSingle(properties.GetProperty("RotZ"))
                );

            Vector3 scale = new Vector3(
                Convert.ToSingle(properties.GetProperty("ScaleX")),
                Convert.ToSingle(properties.GetProperty("ScaleY")),
                Convert.ToSingle(properties.GetProperty("ScaleZ"))
                );

            target.transform.position = position;
            target.transform.rotation = Quaternion.Euler(rotation);
            target.transform.localScale = scale;
        }

        public override IComponentModel Serialize(Transform source)
        {
            return new ComponentModel(typeof(Transform),
                new PropertyModel("PosX", source.position.x),
                new PropertyModel("PosY", source.position.y),
                new PropertyModel("PosZ", source.position.z),

                new PropertyModel("PosX", source.rotation.eulerAngles.x),
                new PropertyModel("PosY", source.rotation.eulerAngles.y),
                new PropertyModel("PosZ", source.rotation.eulerAngles.z),

                new PropertyModel("PosX", source.localScale.x),
                new PropertyModel("PosY", source.localScale.y),
                new PropertyModel("PosZ", source.localScale.z));

        }
    }
}
