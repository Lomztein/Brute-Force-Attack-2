using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Player.Progression.Achievements.Rewards
{
    public interface IAchievementReward
    {
        void Apply();

        void Remove();
    }
}
