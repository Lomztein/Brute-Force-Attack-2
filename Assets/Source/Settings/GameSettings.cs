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
            foreach (var setting in _settings) setting.Init();
            Load();
        }

        public static Setting Get (string identifier)
        {
            return _settings.FirstOrDefault(x => x.Identifier == identifier);
        }

        public static void AddOnChangedListener(string identifier, Setting.OnChangedHandler onChanged)
            => Get(identifier).OnChanged += onChanged;

        public static Setting.OnChangedHandler AddOnChangedListener<T>(string identifier, Action<string, T> onChanged)
        {
            Setting setting = Get(identifier);
            void Handle(string id, object val)
            {
                onChanged(id, (T)val);
            }
            setting.OnChanged += Handle;
            return Handle;
        }

        public static void RemoveOnChangedListener(string identifier, Setting.OnChangedHandler handler)
        {
            Setting setting = Get(identifier);
            setting.OnChanged -= handler;
        }

        public static T GetValue<T>(string identifier, T defaultValue)
        {
            Setting setting = Get(identifier);
            if (setting == null)
                return defaultValue;
            return (T)setting.Get();
        }

        public static void SetValue(string identifier, object value)
        {
            Setting setting = Get(identifier);
            if (setting != null)
                setting.Set(value);
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
