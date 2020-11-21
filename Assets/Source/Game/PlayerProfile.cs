using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Game
{
    public class PlayerProfile
    {
        public static PlayerProfile CurrentProfile = new PlayerProfile(PlayerPrefs.GetString("PlayerProfile", "Default"));

        [ModelProperty]
        public string Name;
        [ModelProperty]
        public ProfileSettings Settings;

        public PlayerProfile() { }

        public PlayerProfile (string name)
        {
            Name = name;
            Settings = new ProfileSettings();
        }
    }
}
