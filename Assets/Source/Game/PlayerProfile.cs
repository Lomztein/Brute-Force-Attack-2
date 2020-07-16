using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Game
{
    public class PlayerProfile : ISerializable
    {
        public static string ProfileFolder = Application.persistentDataPath + "/Profiles";
        public static PlayerProfile CurrentProfile = Load(PlayerPrefs.GetString("PlayerProfile", "Default")) ?? new PlayerProfile(PlayerPrefs.GetString("PlayerProfile", "Default"));

        public string Name;
        public ProfileSettings Settings;

        public PlayerProfile() { }

        public PlayerProfile (string name)
        {
            Name = name;
            Settings = new ProfileSettings();
        }

        public void Save ()
        {
            JToken json = Serialize();
            Directory.CreateDirectory(ProfileFolder);
            File.WriteAllText(GetPath(Name), json.ToString());
        }

        public static PlayerProfile Load(string profileName)
        {
            if (File.Exists(GetPath(profileName)))
            {
                string file = File.ReadAllText(GetPath(profileName));
                PlayerProfile profile = new PlayerProfile();
                profile.Deserialize(JObject.Parse(file));
                return profile;
            }
            return null;
        }

        private static string GetPath(string profileName)
            => ProfileFolder + "/" + profileName + ".json";

        public void Deserialize(JToken source)
        {
            Name = source["Name"].ToString();
            Settings = new ProfileSettings();
            Settings.Deserialize(source["Settings"]);
        }

        public JToken Serialize()
        {
            return new JObject()
            {
                { "Name", Name },
                { "Settings", Settings.Serialize() }
            };
        }
    }
}
