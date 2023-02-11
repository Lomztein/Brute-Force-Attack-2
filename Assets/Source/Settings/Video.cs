using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Settings
{
    public class Video : MonoBehaviour
    {
        void Start()
        {
            GameSettings.AddOnChangedListener<int>("Core.ScreenMode", OnScreenModeChanged);
            GameSettings.AddOnChangedListener<int>("Core.Resolution", OnResolutionChanged);
        }

        private void OnResolutionChanged(string id, int value)
        {
            var resolution = Screen.resolutions[value];
            var screenMode = (FullScreenMode)GameSettings.GetValue("Core.ScreenMode", 0);
            Screen.SetResolution(resolution.width, resolution.height, screenMode);
        }

        private void OnScreenModeChanged(string id, int value)
        {
            var resolution = Screen.resolutions[GameSettings.GetValue("Core.Resolution", Screen.resolutions.ToList().IndexOf(Screen.currentResolution))]; // thanks i hate it
            var screenMode = (FullScreenMode)value;
            Screen.SetResolution(resolution.width, resolution.height, screenMode);
        }
    }
}
