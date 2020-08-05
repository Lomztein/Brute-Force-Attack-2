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
        public override void Deserialize(IComponentModel model, GameObject target)
        {
            DefaultPropertyAssemblers assembler = new DefaultPropertyAssemblers();

            LineRenderer renderer = target.AddComponent<LineRenderer>();
            IPropertyModel[] properties = model.GetProperties();

            renderer.positionCount = properties.GetProperty("PositionCount").Value.ToObject<int>();
            SetPositions(renderer, properties.GetProperty("Positions").Value.Select(x => (Vector3)assembler.Assemble(x, typeof(Vector3))).ToArray());
            renderer.widthCurve = (AnimationCurve)assembler.Assemble(properties.GetProperty("Curve").Value, typeof(AnimationCurve));
            renderer.colorGradient = (Gradient)assembler.Assemble(properties.GetProperty("Colors").Value, typeof(Gradient));

            renderer.numCornerVertices = properties.GetProperty("CornerVerticies").Value.ToObject<int>();
            renderer.numCapVertices = properties.GetProperty("CapVerticies").Value.ToObject<int>();
            renderer.alignment = properties.GetProperty("Alignment").Value.ToObject<LineAlignment>();
            renderer.textureMode = properties.GetProperty("TextureMode").Value.ToObject<LineTextureMode>();
            renderer.shadowBias = properties.GetProperty("ShadowBias").Value.ToObject<float>();
            renderer.generateLightingData = properties.GetProperty("GenerateLighingData").Value.ToObject<bool>();
            renderer.useWorldSpace = properties.GetProperty("UseWorldSpace").Value.ToObject<bool>();
            renderer.shadowCastingMode = properties.GetProperty("ShadowCastingMode").Value.ToObject<ShadowCastingMode>();
            renderer.receiveShadows = properties.GetProperty("RecieveShadows").Value.ToObject<bool>();
            renderer.lightProbeUsage = properties.GetProperty("LightProbeUsage").Value.ToObject<LightProbeUsage>();
            renderer.reflectionProbeUsage = properties.GetProperty("ReflectionProbeUsage").Value.ToObject<ReflectionProbeUsage>();
            renderer.motionVectorGenerationMode = properties.GetProperty("MotionVectorGenerationMode").Value.ToObject<MotionVectorGenerationMode>();
            renderer.allowOcclusionWhenDynamic = properties.GetProperty("AllowOcclusionWhenDynamic").Value.ToObject<bool>();
            renderer.sortingLayerID = properties.GetProperty("SortingLayer").Value.ToObject<int>();
            renderer.sortingOrder = properties.GetProperty("OrderInLayer").Value.ToObject<int>();
        }

        public override IComponentModel Serialize(LineRenderer source)
        {
            DefaultPropertyAssemblers assembler = new DefaultPropertyAssemblers();

            return new ComponentModel(typeof(LineRenderer),
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
                new PropertyModel("UseWorldSpace", new JValue(source.useWorldSpace)),
                new PropertyModel("ShadowCastingMode", new JValue(source.shadowCastingMode)),
                new PropertyModel("RecieveShadows", new JValue(source.receiveShadows)),
                new PropertyModel("LightProbeUsage", new JValue(source.lightProbeUsage)),
                new PropertyModel("ReflectionProbeUsage", new JValue(source.reflectionProbeUsage)),
                new PropertyModel("MotionVectorGenerationMode", new JValue(source.motionVectorGenerationMode)),
                new PropertyModel("AllowOcclusionWhenDynamic", new JValue(source.allowOcclusionWhenDynamic)),
                new PropertyModel("SortingLayer", new JValue(source.sortingLayerID)),
                new PropertyModel("OrderInLayer", new JValue(source.sortingOrder))
                );
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
