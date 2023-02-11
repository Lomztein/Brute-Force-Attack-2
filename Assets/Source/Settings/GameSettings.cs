using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Settings
{
    public static class GameSettings
    {
        private static Setting[] _settings;

        public static void Init ()
        {
            _settings = ContentSystem.Content.GetAll<Setting>("*/Settings/*").ToArray();
            Load();
        }

        public static Setting Get (string identifier)
        {
            return _settings.FirstOrDefault(x => x.Identifier == identifier);
        }

        public static void AddOnChangedListener(string identifier, Action onChanged)
            => Get(identifier).OnChanged += onChanged;

        public static void RemoveOnChangedListener(string identifier, Action onChanged)
            => Get(identifier).OnChanged -= onChanged;

        public static T GetValue<T>(string identifier, T defaultValue)
        {
            Setting setting = Get(identifier);
            if (setting == null)
                return defaultValue;
            return (T)setting.Get();
        }

        public static void Save ()
        {
            foreach (var setting in _settings)
            {
                setting.Save();
            }
        }

        public static void Load ()
        {
            foreach (var setting in _settings)
            {
                setting.Load();
            }
        }
    }
}
