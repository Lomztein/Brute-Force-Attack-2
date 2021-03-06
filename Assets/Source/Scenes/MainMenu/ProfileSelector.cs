﻿using Lomztein.BFA2.Player.Profile;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Style;
using Lomztein.BFA2.UI.Windows;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.MainMenu
{
    public class ProfileSelector : MonoBehaviour, IWindow
    {
        public event Action OnClosed;

        public GameObject ProfileButtonPrefab;
        public Transform ProfileButtonParent;

        private List<GameObject> _buttons = new List<GameObject>();
        public Text NewProfileName;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void Init()
        {
            InstantiateButtons();
        }

        private void SetProfile (PlayerProfile profile)
        {
            ProfileManager.SetProfile(profile);
            PlayerPrefs.SetString("PlayerProfile", profile.Name);
            Close();
        }

        private void CreateNew (string name)
        {
            PlayerProfile newProfile = new PlayerProfile(name);
            SetProfile(newProfile);
            ProfileManager.Save(newProfile);
        }

        public void CreateNew ()
        {
            CreateNew(NewProfileName.text);
        }

        private void InstantiateButtons ()
        {
            foreach (var butt in _buttons)
            {
                Destroy(butt);
            }

            PlayerProfile[] profiles = LoadProfiles();
            foreach (var profile in profiles)
            {
                GameObject newButton = Instantiate(ProfileButtonPrefab, ProfileButtonParent);
                _buttons.Add(newButton);

                Button button = newButton.GetComponent<Button>();
                button.GetComponentInChildren<Text>().text = profile.Name;

                button.onClick.AddListener(() => SetProfile(profile));
            }
        }

        public PlayerProfile[] LoadProfiles()
        {
            if (!Directory.Exists(ProfileManager.ProfileFolder))
            {
                Directory.CreateDirectory(ProfileManager.ProfileFolder);
            }

            string[] files = Directory.GetFiles(ProfileManager.ProfileFolder);
            PlayerProfile[] profiles = new PlayerProfile[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                JObject obj = JObject.Parse(File.ReadAllText(files[i]));
                profiles[i] = ObjectPipeline.BuildObject<PlayerProfile>(obj);
            }

            return profiles;
        }
    }
}
