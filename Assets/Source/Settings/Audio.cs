using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Settings
{
    public class Audio : MonoBehaviour
    {
        private static float _masterVolume = 1f;

        private static float _musicVolume = 1f;
        public static float MusicVolume => _musicVolume * _masterVolume;

        private static float _sfxVolume = 1f;
        public static float SFXVolume => _sfxVolume * _masterVolume;

        private static float _uiVolume = 1f;
        public static float UIVolume => _uiVolume * _masterVolume;

        private void Start()
        {
            _masterVolume = GameSettings.GetValue("Core.MasterVolume", 1f);
            _musicVolume = GameSettings.GetValue("Core.MusicVolume", 1f);
            _sfxVolume = GameSettings.GetValue("Core.SFXVolume", 1f);
            _uiVolume = GameSettings.GetValue("Core.UIVolume", 1f);

            // Add unsubscribers if neccesary, though I higly doubt it will ever be.
            GameSettings.AddOnChangedListener<float>("Core.MasterVolume", OnMasterChanged);
            GameSettings.AddOnChangedListener<float>("Core.MusicVolume", OnMusicChanged);
            GameSettings.AddOnChangedListener<float>("Core.SFXVolume", OnSFXChanged);
            GameSettings.AddOnChangedListener<float>("Core.UIVolume", OnUIChanged);
        }

        private void OnMasterChanged(string id, float val)
            => _masterVolume = val;
        private void OnMusicChanged(string id, float val)
            => _musicVolume = val;
        private void OnSFXChanged(string id, float val)
            => _sfxVolume = val;
        private void OnUIChanged(string id, float val)
            => _uiVolume = val;
    }
}
