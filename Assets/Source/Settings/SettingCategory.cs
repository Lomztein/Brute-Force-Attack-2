using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Settings
{
    [CreateAssetMenu(fileName = "New Setting Category", menuName = "BFA2/Settings/Category")]
    public class SettingCategory : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public int Order;
        [ModelProperty]
        public ContentSpriteReference Sprite;

        public static IEnumerable<SettingCategory> GetCategories()
            => ContentSystem.Content.GetAll<SettingCategory>("*/Settings/Categories/*");

        public static SettingCategory Get(string identifier)
            => GetCategories().FirstOrDefault(x => x.Identifier == identifier);
    }
}
