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

namespace Lomztein.BFA2.Serialization.Assemblers.EngineComponent
{
    public class TrailRendererSerializer : EngineComponentAssembler<TrailRenderer>
    {
        public override void Assemble(IObjectModel model, TrailRenderer target)
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

        public override IObjectModel Disassemble(TrailRenderer source)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
            RendererAssembler baseAssembler = new RendererAssembler();

            return new ObjectModel(typeof(TrailRenderer), baseAssembler.Disassemble(source),
                new ObjectField("Curve", assembler.Disassemble(source.widthCurve, typeof(AnimationCurve))),
                new ObjectField("Time", new ValuePropertyModel(source.time)),
                new ObjectField("MinVertexDistance", new ValuePropertyModel(source.minVertexDistance)),
                new ObjectField("Autodestruct", new ValuePropertyModel(source.autodestruct)),
                new ObjectField("Emitting", new ValuePropertyModel(source.emitting)),
                new ObjectField("Color", assembler.Disassemble(source.colorGradient, typeof (Gradient))),
                new ObjectField("CornerVertices", new ValuePropertyModel(source.numCornerVertices)),
                new ObjectField("CapVertices", new ValuePropertyModel(source.numCapVertices)),
                new ObjectField("Alignment", new ValuePropertyModel(source.alignment)),
                new ObjectField("TextureMode", new ValuePropertyModel(source.textureMode)),
                new ObjectField("GenerateLigtingData", new ValuePropertyModel(source.generateLightingData)),
                new ObjectField("ShadowBias", new ValuePropertyModel(source.shadowBias))
                );
        }
    }
}
