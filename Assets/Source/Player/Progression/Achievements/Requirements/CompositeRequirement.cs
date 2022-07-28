using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class CompositeRequirement : AchievementRequirement
    {
        public override bool Binary => Requirements.Any(x => x.Binary);
        public override float Progression => Requirements.Sum(x => x.Progression) / Requirements.Count();
        public override bool RequirementsMet => Requirements.All(x => x.RequirementsMet);

        [ModelProperty, SerializeReference, SR]
        public IAchievementRequirement[] Requirements;

        public override void End()
        {
            foreach (var requirement in Requirements)
            {
                requirement.End();
            }
        }

        public override void Init()
        {
            foreach (var requirement in Requirements)
            {
                requirement.Init(ProgressPart);
            }
        }

        private void ProgressPart ()
        {
            CheckProgress();
        }

        public override void DeserializeProgress(ValueModel source)
        {
            ArrayModel array = source as ArrayModel;
            var requirements = Requirements.ToArray();
            for (int i = 0; i < requirements.Length; i++)
            {
                if (!ValueModel.IsNull(array[i])) {
                    requirements[i].DeserializeProgress(array[i]);
                }
            }
        }

        public override ValueModel SerializeProgress()
        {
            return new ArrayModel(Requirements.Select(x => x.SerializeProgress()));
        }
    }
}
