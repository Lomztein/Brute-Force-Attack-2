using Lomztein.BFA2.Serialization;
using Sirenix.Serialization;
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

        public abstract void End(Facade facade);

        protected Action _onCompletedCallback;
        public void Init(Facade facade, Action onCompletedCallback)
        {
            _onCompletedCallback = onCompletedCallback;
            Init(facade);
        }

        public abstract void Init(Facade facade);
    }
}
