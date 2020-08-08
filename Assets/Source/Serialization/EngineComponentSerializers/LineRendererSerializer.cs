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
using UnityEngine.Rendering;

namespace Lomztein.BFA2.Serialization.EngineComponentSerializers
{
    public class LineRendererSerializer : EngineComponentSerializerBase<LineRenderer>
    {
        public override void Deserialize(IComponentModel model, LineRenderer target)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
            RendererSerializer baseSerializer = new RendererSerializer();

            IPropertyModel[] properties = model.GetProperties();

            target.positionCount = properties.GetProperty("PositionCount").Value.ToObject<int>();
            SetPositions(target, properties.GetProperty("Positions").Value.Select(x => (Vector3)assembler.Assemble(x, typeof(Vector3))).ToArray());
            target.widthCurve = (AnimationCurve)assembler.Assemble(properties.GetProperty("Curve").Value, typeof(AnimationCurve));
            target.colorGradient = (Gradient)assembler.Assemble(properties.GetProperty("Colors").Value, typeof(Gradient));

            target.numCornerVertices = properties.GetProperty("CornerVerticies").Value.ToObject<int>();
            target.numCapVertices = properties.GetProperty("CapVerticies").Value.ToObject<int>();
            target.alignment = properties.GetProperty("Alignment").Value.ToObject<LineAlignment>();
            target.textureMode = properties.GetProperty("TextureMode").Value.ToObject<LineTextureMode>();
            target.shadowBias = properties.GetProperty("ShadowBias").Value.ToObject<float>();
            target.generateLightingData = properties.GetProperty("GenerateLighingData").Value.ToObject<bool>();
            target.useWorldSpace = properties.GetProperty("UseWorldSpace").Value.ToObject<bool>();

            baseSerializer.Deserialize(model, target);
        }

        public override IComponentModel Serialize(LineRenderer source)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
            RendererSerializer baseSerializer = new RendererSerializer(); // Base serialization could potentially be automated using reflection.

            return new ComponentModel(typeof(LineRenderer), baseSerializer.Serialize(source),
                new PropertyModel("PositionCount", new JValue(source.positionCount)),
                new PropertyModel("Positions", new JArray(GetPositions(source).Select(x => assembler.Dissassemble(x, typeof(Vector3))))),
                new PropertyModel("Curve", assembler.Dissassemble(source.widthCurve, typeof(AnimationCurve))),
                new PropertyModel("Colors", assembler.Dissassemble(source.colorGradient, typeof(Gradient))),
                new PropertyModel("CornerVerticies", new JValue(source.numCornerVertices)),
                new PropertyModel("CapVerticies", new JValue(source.numCapVertices)),
                new PropertyModel("Alignment", new JValue(source.alignment)),
                new PropertyModel("TextureMode", new JValue(source.textureMode)),
                new PropertyModel("ShadowBias", new JValue(source.shadowBias)),
                new PropertyModel("GenerateLighingData", new JValue(source.generateLightingData)),
                new PropertyModel("UseWorldSpace", new JValue(source.useWorldSpace)));
        }

        private Vector3[] GetPositions (LineRenderer source)
        {
            int count = source.positionCount;
            Vector3[] positions = new Vector3[count];
            for (int i = 0; i < count; i++)
            {
                positions[i] = source.GetPosition(i);
            }
            return positions;
        }

        private void SetPositions (LineRenderer target, Vector3[] positions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                target.SetPosition(i, positions[i]);
            }
        }
    }
}
