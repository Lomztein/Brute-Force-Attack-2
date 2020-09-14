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
using UnityEngine.Rendering;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineComponent
{
    public class LineRendererAssembler : EngineComponentAssembler<LineRenderer>
    {
        public override void Assemble(IObjectModel model, LineRenderer target)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
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

        public override IObjectModel Disassemble(LineRenderer source)
        {
            AllPropertyAssemblers assembler = new AllPropertyAssemblers();
            RendererAssembler baseSerializer = new RendererAssembler(); // Base serialization could potentially be automated using reflection.

            return new ObjectModel(typeof(LineRenderer), baseSerializer.Disassemble(source),
                new ObjectField("PositionCount", new ValuePropertyModel(source.positionCount)),
                new ObjectField("Positions", new ArrayPropertyModel(typeof (Vector3[]), GetPositions(source).Select(x => assembler.Disassemble(x, typeof(Vector3))))),
                new ObjectField("Curve", assembler.Disassemble(source.widthCurve, typeof(AnimationCurve))),
                new ObjectField("Colors", assembler.Disassemble(source.colorGradient, typeof(Gradient))),
                new ObjectField("CornerVerticies", new ValuePropertyModel(source.numCornerVertices)),
                new ObjectField("CapVerticies", new ValuePropertyModel(source.numCapVertices)),
                new ObjectField("Alignment", new ValuePropertyModel(source.alignment)),
                new ObjectField("TextureMode", new ValuePropertyModel(source.textureMode)),
                new ObjectField("ShadowBias", new ValuePropertyModel(source.shadowBias)),
                new ObjectField("GenerateLighingData", new ValuePropertyModel(source.generateLightingData)),
                new ObjectField("UseWorldSpace", new ValuePropertyModel(source.useWorldSpace)));
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
