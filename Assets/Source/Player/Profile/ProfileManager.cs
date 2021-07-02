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
        private const string DEFAULT_NAME = "Default";
        public static string ProfileFolder => Application.persistentDataPath + "/Profiles";
        public static PlayerProfile CurrentProfile { get; private set; } = LoadOrDefault(PlayerPrefs.GetString("PlayerProfile", DEFAULT_NAME));

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
            // First, check if requested profile exists.
            PlayerProfile profile = Load(profileName);
            if (profile == null)
            {
                // Check if defualt profile exists.
                profile = Load(DEFAULT_NAME);
                if (profile == null)
                {
                    // If none at all, create new default.
                    profile = new PlayerProfile(DEFAULT_NAME);
                    Save(profile);
                }
            }
            return profile;
        }
    }
}
