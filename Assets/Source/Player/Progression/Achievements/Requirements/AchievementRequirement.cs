using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public abstract class AchievementRequirement : IAchievementRequirement
    {
        public virtual bool Binary => true;
        public virtual float Progression => BinaryProgression();
        public virtual bool RequirementsMet => MeetsRequirements();

        protected virtual bool MeetsRequirements() => false;

        public abstract void End();

        private Action _onProgressedCallback;

        public void Init(Action onProgressedCallback)
        {
            _onProgressedCallback = onProgressedCallback;
            Init();
        }

        public abstract void Init();

        protected float BinaryProgression()
            => RequirementsMet ? 1f : 0f;

        public virtual ValueModel SerializeProgress()
        {
            return new NullModel();
        }

        protected void CheckRequirements ()
        {
            _onProgressedCallback();
        }

        public virtual void DeserializeProgress(ValueModel source)
        {
        }
    }
}
