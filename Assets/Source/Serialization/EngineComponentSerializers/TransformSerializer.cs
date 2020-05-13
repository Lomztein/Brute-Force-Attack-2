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

            Vector3 position = new Vector3(
                properties.GetProperty("Position").Value["X"].ToObject<float>(),
                properties.GetProperty("Position").Value["Y"].ToObject<float>(),
                properties.GetProperty("Position").Value["Z"].ToObject<float>()
                );

            Vector3 rotation = new Vector3(
                properties.GetProperty("Rotation").Value["X"].ToObject<float>(),
                properties.GetProperty("Rotation").Value["Y"].ToObject<float>(),
                properties.GetProperty("Rotation").Value["Z"].ToObject<float>()
                );

            Vector3 scale = new Vector3(
                properties.GetProperty("Scale").Value["X"].ToObject<float>(),
                properties.GetProperty("Scale").Value["Y"].ToObject<float>(),
                properties.GetProperty("Scale").Value["Z"].ToObject<float>()
                );

            target.transform.position = position;
            target.transform.rotation = Quaternion.Euler(rotation);
            target.transform.localScale = scale;
        }

        public override IComponentModel Serialize(Transform source)
        {
            return new ComponentModel(typeof(Transform),
                new PropertyModel("Position", new JObject() {
                    { "X", source.position.x },
                    { "Y", source.position.y },
                    { "Z", source.position.z }
                }),
                new PropertyModel("Rotation", new JObject() {
                    { "X", source.rotation.eulerAngles.x },
                    { "Y", source.rotation.eulerAngles.y },
                    { "Z", source.rotation.eulerAngles.z }
                }),
                new PropertyModel("Scale", new JObject() {
                    { "X", source.localScale.x },
                    { "Y", source.localScale.y },
                    { "Z", source.localScale.z }
                }));
        }
    }
}
