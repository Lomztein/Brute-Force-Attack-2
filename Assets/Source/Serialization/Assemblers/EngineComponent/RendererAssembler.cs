using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineComponent
{
    public class RendererAssembler : EngineComponentAssembler<Renderer>
    {
        public override void Assemble(ObjectModel model, Renderer target)
        {
            var properties = model.GetProperties();

            target.shadowCastingMode = model.GetValue<ShadowCastingMode>("ShadowCastingMode");
            target.receiveShadows = model.GetValue<bool>("RecieveShadows");
            target.lightProbeUsage = model.GetValue<LightProbeUsage>("LightProbeUsage");
            target.reflectionProbeUsage = model.GetValue<ReflectionProbeUsage>("ReflectionProbeUsage");
            target.motionVectorGenerationMode = model.GetValue<MotionVectorGenerationMode>("MotionVectorGenerationMode");
            target.allowOcclusionWhenDynamic = model.GetValue<bool>("AllowOcclusionWhenDynamic");
            target.sortingLayerID = model.GetValue<int>("SortingLayer");
            target.sortingOrder = model.GetValue<int>("OrderInLayer");
        }

        public override ObjectModel Disassemble(Renderer source)
        {
            return new ObjectModel(typeof(Renderer),
                new ObjectField("ShadowCastingMode", new PrimitivePropertyModel(source.shadowCastingMode)),
                new ObjectField("RecieveShadows", new PrimitivePropertyModel(source.receiveShadows)),
                new ObjectField("LightProbeUsage", new PrimitivePropertyModel(source.lightProbeUsage)),
                new ObjectField("ReflectionProbeUsage", new PrimitivePropertyModel(source.reflectionProbeUsage)),
                new ObjectField("MotionVectorGenerationMode", new PrimitivePropertyModel(source.motionVectorGenerationMode)),
                new ObjectField("AllowOcclusionWhenDynamic", new PrimitivePropertyModel(source.allowOcclusionWhenDynamic)),
                new ObjectField("SortingLayer", new PrimitivePropertyModel(source.sortingLayerID)),
                new ObjectField("OrderInLayer", new PrimitivePropertyModel(source.sortingOrder))
            );
        }
    }
}
