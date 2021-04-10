using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Style;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Profile
{
    public class PlayerProfile
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public UIStyle UIStyle = UIStyle.Default();
        [ModelProperty]
        public List<PlayerAchievementStatus> AchievementStatus = new List<PlayerAchievementStatus>();

        public PlayerProfile() { }

        public PlayerProfile (string name)
        {
            Name = name;
        }

        public void MutateAchievementStatus (string identifier, Action<PlayerAchievementStatus> action)
        {
            action(GetAchievementStatus(identifier));
        }

        public PlayerAchievementStatus GetAchievementStatus(string identifier)
        {
            PlayerAchievementStatus status = AchievementStatus.FirstOrDefault(x => x.Identifier == identifier);
            if (status == null)
            {
                status = new PlayerAchievementStatus(identifier);
                AchievementStatus.Add(status);
            }
            return status;
        }
    }
}
