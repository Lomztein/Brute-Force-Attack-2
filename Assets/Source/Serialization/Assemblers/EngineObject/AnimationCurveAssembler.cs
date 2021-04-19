using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public class AnimationCurveAssembler : EngineObjectAssemblerBase<AnimationCurve>
    {
        public override AnimationCurve AssembleValue(ObjectModel value, AssemblyContext context)
        {
            ArrayModel array = value.GetProperty<ArrayModel>("Keyframes");
            AnimationCurve curve = new AnimationCurve(array.Select(x => AssembleKeyframe(x as ObjectModel)).ToArray());
            return curve;
        }

        private Keyframe AssembleKeyframe (ObjectModel frameModel)
        {
            return new Keyframe(
                    frameModel.GetValue<float>("Time"),
                    frameModel.GetValue<float>("Value"),
                    frameModel.GetValue<float>("InTangent"),
                    frameModel.GetValue<float>("OutTangent"),
                    frameModel.GetValue<float>("InWeight"),
                    frameModel.GetValue<float>("OutWeight")
                    );
        }

        public override ObjectModel DisassembleValue(AnimationCurve value, DisassemblyContext context)
        {
            IEnumerable<ObjectModel> frames = value.keys.Select(x => DissasembleKeyframe(x));
            return new ObjectModel(
                new ObjectField("Keyframes", new ArrayModel(frames)));
        }

        private ObjectModel DissasembleKeyframe (Keyframe frame)
        {
            return new ObjectModel(
                new ObjectField ("Time", new PrimitiveModel(frame.time)),
                new ObjectField("Value", new PrimitiveModel(frame.value)),
                new ObjectField("InTangent", new PrimitiveModel(frame.inTangent)),
                new ObjectField("OutTangent", new PrimitiveModel(frame.outTangent)),
                new ObjectField("InWeight", new PrimitiveModel(frame.inWeight)),
                new ObjectField("OutWeight", new PrimitiveModel(frame.outWeight)));
        }
    }
}
