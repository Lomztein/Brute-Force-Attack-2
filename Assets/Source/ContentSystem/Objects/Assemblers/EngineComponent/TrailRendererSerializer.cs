using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Assemblers.Property;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Content.Assemblers.EngineComponent
{
    public class TrailRendererSerializer : EngineComponentAssembler<TrailRenderer>
    {
        public override void Assemble(ObjectModel model, TrailRenderer target)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
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

            return new ObjectModel(typeof(TrailRenderer), baseAssembler.Disassemble(source),
                new ObjectField("Curve", PropertyModelFactory.Create (source.widthCurve)),
                new ObjectField("Time", new PrimitivePropertyModel(source.time)),
                new ObjectField("MinVertexDistance", new PrimitivePropertyModel(source.minVertexDistance)),
                new ObjectField("Autodestruct", new PrimitivePropertyModel(source.autodestruct)),
                new ObjectField("Emitting", new PrimitivePropertyModel(source.emitting)),
                new ObjectField("Color", PropertyModelFactory.Create(source.colorGradient)),
                new ObjectField("CornerVertices", new PrimitivePropertyModel(source.numCornerVertices)),
                new ObjectField("CapVertices", new PrimitivePropertyModel(source.numCapVertices)),
                new ObjectField("Alignment", new PrimitivePropertyModel(source.alignment)),
                new ObjectField("TextureMode", new PrimitivePropertyModel(source.textureMode)),
                new ObjectField("GenerateLigtingData", new PrimitivePropertyModel(source.generateLightingData)),
                new ObjectField("ShadowBias", new PrimitivePropertyModel(source.shadowBias))
                );
        }
    }
}
