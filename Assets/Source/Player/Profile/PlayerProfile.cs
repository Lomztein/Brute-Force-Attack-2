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
        private List<string> _completedAchievements = new List<string>();

        public PlayerProfile() { }

        public PlayerProfile (string name)
        {
            Name = name;
        }

        public void AddCompletedAchievement (string identifier)
        {
            _completedAchievements.Add(identifier);
        }

        public bool HasCompletedAchievement(string identifier) => _completedAchievements.Contains(identifier);
    }
}
