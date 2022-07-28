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
        public abstract bool Binary { get; }
        public abstract float Progression { get; }
        public abstract bool RequirementsMet { get; }

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

        protected void CheckProgress ()
        {
            _onProgressedCallback();
        }

        public virtual void DeserializeProgress(ValueModel source)
        {
        }
    }
}
