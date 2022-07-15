using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Collectables
{
    [CreateAssetMenu(fileName = "NewRarity", menuName = "BFA2/Collectables/Rarity")]
    public class Rarity : ScriptableObject, IIdentifiable, INamed
    {
        private const string RARITY_CONTENT_PATH = "*/Rarities/*";

        [SerializeField, ModelProperty]
        private string _identifier;
        public string Identifier => _identifier;
        [SerializeField, ModelProperty]
        private string _name;
        public string Name => _name;
        [SerializeField, ModelProperty]
        private string _description;
        public string Description => _description;
        [ModelProperty]
        public Color Color;

        public static Rarity[] GetRarities() => Content.GetAll<Rarity>(RARITY_CONTENT_PATH).ToArray();
        public static Rarity GetRarity(string identifier) => GetRarities().FirstOrDefault(x => x.Identifier == identifier);
    }
}
