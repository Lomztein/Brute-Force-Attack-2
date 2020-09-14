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
        public override AnimationCurve AssembleValue(IObjectModel value)
        {
            ArrayPropertyModel array = value.GetProperty<ArrayPropertyModel>("Keyframes");
            AnimationCurve curve = new AnimationCurve(array.Select(x => AssembleKeyframe((x as ObjectPropertyModel).Model)).ToArray());
            return curve;
        }

        private Keyframe AssembleKeyframe (IObjectModel frameModel)
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

        public override IObjectModel DissasembleValue(AnimationCurve value)
        {
            IEnumerable<IObjectModel> frames = value.keys.Select(x => DissasembleKeyframe(x));
            return new ObjectModel(typeof(AnimationCurve), new ArrayPropertyModel(typeof(AnimationCurve[]), "Keyframes", frames.Select(x => new ObjectPropertyModel(null, x)).ToArray()));
        }

        private IObjectModel DissasembleKeyframe (Keyframe frame)
        {
            return new ObjectModel(typeof(Keyframe),
                new ValuePropertyModel("Time", frame.time),
                new ValuePropertyModel("Value", frame.value),
                new ValuePropertyModel("InTangent", frame.inTangent),
                new ValuePropertyModel("OutTangent", frame.outTangent),
                new ValuePropertyModel("InWeight", frame.inWeight),
                new ValuePropertyModel("OutWeight", frame.outWeight));
        }
    }
}
