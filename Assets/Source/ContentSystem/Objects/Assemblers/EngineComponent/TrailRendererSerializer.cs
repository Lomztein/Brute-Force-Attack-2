using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Content.Assemblers.EngineComponent
{
    public class TrailRendererSerializer : EngineComponentAssembler<TrailRenderer>
    {
        public override void Assemble(ObjectModel model, TrailRenderer target)
        {
            ValueAssembler assembler = new ValueAssembler();
            RendererAssembler baseAssembler = new RendererAssembler();

            target.widthCurve = (AnimationCurve)assembler.Assemble(model.GetProperty("Curve"), typeof(AnimationCurve));
            target.time = model.GetValue<float>("Time");
            target.minVertexDistance = model.GetValue<float>("MinVertexDistance");
            target.autodestruct = model.GetValue<bool>("Autodestruct");
            target.emitting = model.GetValue<bool>("Emitting");
            target.colorGradient = (Gradient)assembler.Assemble(model.GetProperty("Color"), typeof(Gradient));
            target.numCornerVertices = model.GetValue<int>("CornerVertices");
            target.numCapVertices = model.GetValue<int>("CapVertices");
            target.alignment = model.GetValue<LineAlignment>("Alignment");
            target.textureMode = model.GetValue<LineTextureMode>("TextureMode");
            target.generateLightingData = model.GetValue<bool>("GenerateLigtingData");
            target.shadowBias = model.GetValue<float>("ShadowBias");

            baseAssembler.Assemble(model, target);
        }

        public override ObjectModel Disassemble(TrailRenderer source)
        {
            RendererAssembler baseAssembler = new RendererAssembler();

            return new ObjectModel(baseAssembler.Disassemble(source),
                new ObjectField("Curve", ValueModelFactory.Create (source.widthCurve)),
                new ObjectField("Time", new PrimitiveModel(source.time)),
                new ObjectField("MinVertexDistance", new PrimitiveModel(source.minVertexDistance)),
                new ObjectField("Autodestruct", new PrimitiveModel(source.autodestruct)),
                new ObjectField("Emitting", new PrimitiveModel(source.emitting)),
                new ObjectField("Color", ValueModelFactory.Create(source.colorGradient)),
                new ObjectField("CornerVertices", new PrimitiveModel(source.numCornerVertices)),
                new ObjectField("CapVertices", new PrimitiveModel(source.numCapVertices)),
                new ObjectField("Alignment", new PrimitiveModel(source.alignment)),
                new ObjectField("TextureMode", new PrimitiveModel(source.textureMode)),
                new ObjectField("GenerateLigtingData", new PrimitiveModel(source.generateLightingData)),
                new ObjectField("ShadowBias", new PrimitiveModel(source.shadowBias))
                );
        }
    }
}
