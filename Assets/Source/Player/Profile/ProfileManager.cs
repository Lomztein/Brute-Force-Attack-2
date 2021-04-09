using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Profile
{
    public static class ProfileManager
    {
        public static string ProfileFolder => Application.persistentDataPath + "/Profiles";
        public static PlayerProfile CurrentProfile { get; private set; } = LoadOrDefault(PlayerPrefs.GetString("PlayerProfile", "Default"));

        public static event Action<PlayerProfile, PlayerProfile> OnProfileChanged;

        private static string GetPath(string profileName)
            => ProfileFolder + "/" + profileName + ".json";

        public static void SetProfile (PlayerProfile newProfile)
        {
            PlayerProfile old = CurrentProfile;
            CurrentProfile = newProfile;
            OnProfileChanged?.Invoke(old, CurrentProfile);
        }

        public static void Save(PlayerProfile profile)
        {
            JToken json = ObjectPipeline.UnbuildObject(profile, true);
            Directory.CreateDirectory(ProfileFolder);
            File.WriteAllText(GetPath(profile.Name), json.ToString());
        }

        public static void SaveCurrent() => Save(CurrentProfile);

        public static PlayerProfile Load(string profileName)
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

        public static PlayerProfile LoadOrDefault(string profileName)
        {
            PlayerProfile profile = Load(profileName);
            if (profile == null)
            {
                profile = new PlayerProfile("Default");
                Save(profile);
            }
            return profile;
        }
    }
}
