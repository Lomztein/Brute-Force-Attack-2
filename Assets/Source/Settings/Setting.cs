using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Settings
{
    public abstract class Setting : ScriptableObject
    {
        private enum ValueType { Int, Float, String }

        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public int Order;
        [ModelAssetReference]
        public SettingCategory Category;

        public event Action OnChanged;

        public static IEnumerable<Setting> LoadSettings()
            => ContentSystem.Content.GetAll<Setting>("*/Settings/*");

        protected object Value { get; private set; }

        protected abstract object GetDefaultValue();

        public object Get()
            => Value?? GetDefaultValue();

        public T Get<T>()
            => (T)Get();

        public void Set(object newValue)
        {
            Value = newValue;
            OnChanged?.Invoke();
        }

        private ValueType GetValueType()
        {
            Type valueType = Get().GetType();
            if (valueType == typeof(int)) return ValueType.Int;
            if (valueType == typeof(float)) return ValueType.Float;
            if (valueType == typeof(string)) return ValueType.String;
            throw new InvalidOperationException($"Value type {valueType.Name} not supported. Please convert to float, int, or string.");
        }

        public void Save ()
        {
            var valueType = GetValueType();

            switch (valueType)
            {
                case ValueType.Int:
                    PlayerPrefs.SetInt(Identifier, (int)Value);
                    break;

                case ValueType.Float:
                    PlayerPrefs.SetFloat(Identifier, (float)Value);
                    break;

                case ValueType.String:
                    PlayerPrefs.SetString(Identifier, (string)Value);
                    break;
            }
        }

        public void Load ()
        {
            var valueType = GetValueType();
            var defaultValue = GetDefaultValue();

            switch (valueType)
            {
                case ValueType.Int:
                    Set(PlayerPrefs.GetInt(Identifier, (int)defaultValue));
                    break;

                case ValueType.Float:
                    Set(Value = PlayerPrefs.GetFloat(Identifier, (float)defaultValue));
                    break;

                case ValueType.String:
                    Set(Value = PlayerPrefs.GetString(Identifier, (string)defaultValue));
                    break;
            }
        }
    }
}
