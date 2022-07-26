using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.Resources
{
    [CreateAssetMenu(fileName = "NewResource", menuName = "BFA2/Resource")]
    public class Resource : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Shorthand;
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public Color Color;
        [ModelProperty]
        public int BinaryValue;
        [ModelProperty]
        public ContentSpriteReference Sprite;

        public static Resource[] GetResources() => Content.GetAll<Resource>("*/Resources/*").ToArray();
        public static Resource GetResource(string identifier) => GetResources().FirstOrDefault(x => x.Identifier == identifier);

        public override bool Equals(object other)
        {
            if (other is Resource otherResource)
            {
                return Identifier == otherResource.Identifier;
            }
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(Identifier))
            {
                return base.GetHashCode();
            }
            return Identifier.GetHashCode();
        }
    }
}
