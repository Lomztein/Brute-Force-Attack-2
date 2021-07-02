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
        public abstract bool Completed { get; }

        public abstract void End();

        protected Action _onCompletedCallback;
        protected Action _onProgressedCallback;

        public void Init(Action onCompletedCallback, Action onProgressedCallback)
        {
            _onCompletedCallback = onCompletedCallback;
            _onProgressedCallback = onProgressedCallback;

            Init();
        }

        public abstract void Init();

        public virtual ValueModel SerializeProgress()
        {
            return new NullModel();
        }

        public virtual void DeserializeProgress(ValueModel source)
        {
        }
    }
}
