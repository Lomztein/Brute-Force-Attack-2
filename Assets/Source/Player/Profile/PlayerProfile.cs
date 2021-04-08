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

namespace Lomztein.BFA2.Game
{
    public class PlayerProfile
    {
        public static string ProfileFolder => Application.persistentDataPath + "/Profiles";

        public static PlayerProfile CurrentProfile = LoadOrDefault(PlayerPrefs.GetString("PlayerProfile", "Default"));

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


        private static string GetPath(string profileName)
            => ProfileFolder + "/" + profileName + ".json";

        public void Save()
        {
            JToken json = ObjectPipeline.UnbuildObject(this, true);
            Directory.CreateDirectory(ProfileFolder);
            File.WriteAllText(GetPath(Name), json.ToString());
        }

        public static PlayerProfile Load (string profileName)
        {
            try
            {
                JToken json = JToken.Parse(File.ReadAllText(GetPath(profileName)));
                return ObjectPipeline.BuildObject<PlayerProfile>(json);
            }
            catch
            {
                return null;
            }
        }

        public static PlayerProfile LoadOrDefault (string profileName)
        {
            PlayerProfile profile = Load(profileName);
            if (profile == null)
            {
                profile = new PlayerProfile("Default");
                profile.Save();
            }
            return profile;
        }
    }
}
