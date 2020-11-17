using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public class AnimationCurveAssembler : EngineObjectAssemblerBase<AnimationCurve>
    {
        public override AnimationCurve AssembleValue(ObjectModel value)
        {
            ArrayPropertyModel array = value.GetProperty<ArrayPropertyModel>("Keyframes");
            AnimationCurve curve = new AnimationCurve(array.Select(x => AssembleKeyframe((x as ComplexPropertyModel).Model)).ToArray());
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

        public override ObjectModel DisassembleValue(AnimationCurve value)
        {
            IEnumerable<ObjectModel> frames = value.keys.Select(x => DissasembleKeyframe(x));
            return new ObjectModel(typeof(AnimationCurve),
                new ObjectField("Keyframes", new ArrayPropertyModel(typeof(Keyframe[]), frames.Select(x => new ComplexPropertyModel(x)).ToArray())));
        }

        private ObjectModel DissasembleKeyframe (Keyframe frame)
        {
            return new ObjectModel(typeof(Keyframe),
                new ObjectField ("Time", new PrimitivePropertyModel(frame.time)),
                new ObjectField ("Value", new PrimitivePropertyModel(frame.value)),
                new ObjectField ("InTangent", new PrimitivePropertyModel(frame.inTangent)),
                new ObjectField ("OutTangent", new PrimitivePropertyModel(frame.outTangent)),
                new ObjectField ("InWeight", new PrimitivePropertyModel(frame.inWeight)),
                new ObjectField ("OutWeight", new PrimitivePropertyModel(frame.outWeight))).MakeImplicit();
        }
    }
}
