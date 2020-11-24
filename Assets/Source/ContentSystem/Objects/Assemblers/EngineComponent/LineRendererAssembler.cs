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
    public class LineRendererAssembler : EngineComponentAssembler<LineRenderer>
    {
        public override void Assemble(ObjectModel model, LineRenderer target)
        {
            ValueAssembler assembler = new ValueAssembler();
            RendererAssembler baseSerializer = new RendererAssembler();

            target.positionCount = model.GetValue<int>("PositionCount");
            SetPositions(target, model.GetArray("Positions").Select(x => (Vector3)assembler.Assemble(x, typeof(Vector3))).ToArray());
            target.widthCurve = (AnimationCurve)assembler.Assemble(model.GetProperty("Curve"), typeof(AnimationCurve));
            target.colorGradient = (Gradient)assembler.Assemble(model.GetProperty("Colors"), typeof(Gradient));

            target.numCornerVertices = model.GetValue<int>("CornerVerticies");
            target.numCapVertices = model.GetValue<int>("CapVerticies");
            target.alignment = model.GetValue<LineAlignment>("Alignment");
            target.textureMode = model.GetValue<LineTextureMode>("TextureMode");
            target.shadowBias = model.GetValue<float>("ShadowBias");
            target.generateLightingData = model.GetValue<bool>("GenerateLighingData");
            target.useWorldSpace = model.GetValue<bool>("UseWorldSpace");

            baseSerializer.Assemble(model, target);
        }

        public override ObjectModel Disassemble(LineRenderer source)
        {
            ValueAssembler assembler = new ValueAssembler();
            RendererAssembler baseSerializer = new RendererAssembler(); // Base serialization could potentially be automated using reflection.

            return new ObjectModel(baseSerializer.Disassemble(source),
                new ObjectField("PositionCount", new PrimitiveModel(source.positionCount)),
                new ObjectField("Positions", new ArrayModel(GetPositions(source).Select(x => ValueModelFactory.Create(x)))),
                new ObjectField("Curve", ValueModelFactory.Create(source.widthCurve)),
                new ObjectField("Colors", ValueModelFactory.Create(source.colorGradient)),
                new ObjectField("CornerVerticies", new PrimitiveModel(source.numCornerVertices)),
                new ObjectField("CapVerticies", new PrimitiveModel(source.numCapVertices)),
                new ObjectField("Alignment", new PrimitiveModel(source.alignment)),
                new ObjectField("TextureMode", new PrimitiveModel(source.textureMode)),
                new ObjectField("ShadowBias", new PrimitiveModel(source.shadowBias)),
                new ObjectField("GenerateLighingData", new PrimitiveModel(source.generateLightingData)),
                new ObjectField("UseWorldSpace", new PrimitiveModel(source.useWorldSpace)));
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
