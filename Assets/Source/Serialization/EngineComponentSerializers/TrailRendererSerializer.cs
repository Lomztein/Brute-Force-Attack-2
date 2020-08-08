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
        public override void Deserialize(IComponentModel model, TrailRenderer target)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
            RendererSerializer baseSerializer = new RendererSerializer();

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

        public override IComponentModel Serialize(TrailRenderer source)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
            RendererSerializer baseSerializer = new RendererSerializer();

            return new ComponentModel(typeof(TrailRenderer), baseSerializer.Serialize(source),
                new PropertyModel("Curve", assembler.Dissassemble(source.widthCurve, typeof(AnimationCurve))),
                new PropertyModel("Time", new JValue(source.time)),
                new PropertyModel("MinVertexDistance", new JValue(source.minVertexDistance)),
                new PropertyModel("Autodestruct", new JValue(source.autodestruct)),
                new PropertyModel("Emitting", new JValue(source.emitting)),
                new PropertyModel("Color", assembler.Dissassemble(source.colorGradient, typeof (Gradient))),
                new PropertyModel("CornerVertices", new JValue(source.numCornerVertices)),
                new PropertyModel("CapVertices", new JValue(source.numCapVertices)),
                new PropertyModel("Alignment", new JValue(source.alignment)),
                new PropertyModel("TextureMode", new JValue(source.textureMode)),
                new PropertyModel("GenerateLigtingData", new JValue(source.generateLightingData)),
                new PropertyModel("ShadowBias", new JValue(source.shadowBias))
                );
        }
    }
}
