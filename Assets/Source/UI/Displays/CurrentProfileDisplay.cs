﻿using Lomztein.BFA2.LocalizationSystem;
using Lomztein.BFA2.Player.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays
{
    public class CurrentProfileDisplay : MonoBehaviour
    {
        public Text Text;

        public void Update()
        {
            Text.text = Localization.Get("MENU_PROFILE_BUTTON", ProfileManager.CurrentProfile.Name);
        }
    }
}
