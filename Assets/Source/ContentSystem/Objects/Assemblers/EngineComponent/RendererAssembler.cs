using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Lomztein.BFA2.ContentSystem.Assemblers.EngineComponent
{
    public class RendererAssembler : EngineComponentAssembler<Renderer>
    {
        public override void Assemble(ObjectModel model, Renderer target, AssemblyContext context)
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

        public override ObjectModel Disassemble(Renderer source, DisassemblyContext context)
        {
            return new ObjectModel(
                new ObjectField("ShadowCastingMode", new PrimitiveModel(source.shadowCastingMode)),
                new ObjectField("RecieveShadows", new PrimitiveModel(source.receiveShadows)),
                new ObjectField("LightProbeUsage", new PrimitiveModel(source.lightProbeUsage)),
                new ObjectField("ReflectionProbeUsage", new PrimitiveModel(source.reflectionProbeUsage)),
                new ObjectField("MotionVectorGenerationMode", new PrimitiveModel(source.motionVectorGenerationMode)),
                new ObjectField("AllowOcclusionWhenDynamic", new PrimitiveModel(source.allowOcclusionWhenDynamic)),
                new ObjectField("SortingLayer", new PrimitiveModel(source.sortingLayerID)),
                new ObjectField("OrderInLayer", new PrimitiveModel(source.sortingOrder))
            );
        }
    }
}
