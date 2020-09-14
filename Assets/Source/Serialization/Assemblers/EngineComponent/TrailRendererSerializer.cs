using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Assemblers.Property;
using Lomztein.BFA2.Serialization.Models.Component;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public class TrailRendererSerializer : EngineComponentSerializerBase<TrailRenderer>
    {
        public override void Deserialize(IObjectModel model, TrailRenderer target)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
            RendererAssembler baseSerializer = new RendererAssembler();

            IPropertyModel[] properties = model.GetProperties();

            target.widthCurve = (AnimationCurve)assembler.Assemble(properties.GetProperty("Curve").Value, typeof(AnimationCurve));
            target.time = properties.GetProperty("Time").ToObject<float>();
            target.minVertexDistance = properties.GetProperty("MinVertexDistance").ToObject<float>();
            target.autodestruct = properties.GetProperty("Autodestruct").ToObject<bool>();
            target.emitting = properties.GetProperty("Emitting").ToObject<bool>();
            target.colorGradient = (Gradient)assembler.Assemble(properties.GetProperty("Color").Value, typeof(Gradient));
            target.numCornerVertices = properties.GetProperty("CornerVertices").ToObject<int>();
            target.numCapVertices = properties.GetProperty("CapVertices").ToObject<int>();
            target.alignment = properties.GetProperty("Alignment").ToObject<LineAlignment>();
            target.textureMode = properties.GetProperty("TextureMode").ToObject<LineTextureMode>();
            target.generateLightingData = properties.GetProperty("GenerateLigtingData").ToObject<bool>();
            target.shadowBias = properties.GetProperty("ShadowBias").ToObject<float>();

            baseSerializer.Deserialize(model, target);
        }

        public override IObjectModel Serialize(TrailRenderer source)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
            RendererAssembler baseSerializer = new RendererAssembler();

            return new ObjectModel(typeof(TrailRenderer), baseSerializer.Serialize(source),
                new ValuePropertyModel("Curve", assembler.Disassemble(source.widthCurve, typeof(AnimationCurve))),
                new ValuePropertyModel("Time", new JValue(source.time)),
                new ValuePropertyModel("MinVertexDistance", new JValue(source.minVertexDistance)),
                new ValuePropertyModel("Autodestruct", new JValue(source.autodestruct)),
                new ValuePropertyModel("Emitting", new JValue(source.emitting)),
                new ValuePropertyModel("Color", assembler.Disassemble(source.colorGradient, typeof (Gradient))),
                new ValuePropertyModel("CornerVertices", new JValue(source.numCornerVertices)),
                new ValuePropertyModel("CapVertices", new JValue(source.numCapVertices)),
                new ValuePropertyModel("Alignment", new JValue(source.alignment)),
                new ValuePropertyModel("TextureMode", new JValue(source.textureMode)),
                new ValuePropertyModel("GenerateLigtingData", new JValue(source.generateLightingData)),
                new ValuePropertyModel("ShadowBias", new JValue(source.shadowBias))
                );
        }
    }
}
