using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models.Component;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public class RendererSerializer : EngineComponentSerializerBase<Renderer>
    {
        public override void Deserialize(IComponentModel model, Renderer target)
        {
            var properties = model.GetProperties();

            target.shadowCastingMode = properties.GetProperty("ShadowCastingMode").Value.ToObject<ShadowCastingMode>();
            target.receiveShadows = properties.GetProperty("RecieveShadows").Value.ToObject<bool>();
            target.lightProbeUsage = properties.GetProperty("LightProbeUsage").Value.ToObject<LightProbeUsage>();
            target.reflectionProbeUsage = properties.GetProperty("ReflectionProbeUsage").Value.ToObject<ReflectionProbeUsage>();
            target.motionVectorGenerationMode = properties.GetProperty("MotionVectorGenerationMode").Value.ToObject<MotionVectorGenerationMode>();
            target.allowOcclusionWhenDynamic = properties.GetProperty("AllowOcclusionWhenDynamic").Value.ToObject<bool>();
            target.sortingLayerID = properties.GetProperty("SortingLayer").Value.ToObject<int>();
            target.sortingOrder = properties.GetProperty("OrderInLayer").Value.ToObject<int>();
        }

        public override IComponentModel Serialize(Renderer source)
        {
            return new ComponentModel(typeof(Renderer),
                new PropertyModel("ShadowCastingMode", new JValue(source.shadowCastingMode)),
                new PropertyModel("RecieveShadows", new JValue(source.receiveShadows)),
                new PropertyModel("LightProbeUsage", new JValue(source.lightProbeUsage)),
                new PropertyModel("ReflectionProbeUsage", new JValue(source.reflectionProbeUsage)),
                new PropertyModel("MotionVectorGenerationMode", new JValue(source.motionVectorGenerationMode)),
                new PropertyModel("AllowOcclusionWhenDynamic", new JValue(source.allowOcclusionWhenDynamic)),
                new PropertyModel("SortingLayer", new JValue(source.sortingLayerID)),
                new PropertyModel("OrderInLayer", new JValue(source.sortingOrder))
            );
        }
    }
}
