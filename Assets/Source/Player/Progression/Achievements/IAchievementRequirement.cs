using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Player.Progression.Achievements
{
    public interface IAchievementRequirement
    {
        string Name { get; }
        string Description { get; }

        bool Binary { get; }
        float Progression { get; }

        bool Completed { get; }

        void Init(Facade facade, Action onCompletedCallback);

        void End(Facade facade);
    }
}
