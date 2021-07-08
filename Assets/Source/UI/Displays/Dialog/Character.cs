using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Dialog
{
    [Serializable, CreateAssetMenu(fileName = "New DialogTree", menuName = "BFA2/Dialog/Character")]
    public class Character : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public Expression[] Expressions;

        public Texture2D GetAvatar(string expressionKey) => Expressions.First(x => x.Key == expressionKey).Texture;

        [Serializable]
        public class Expression
        {
            [ModelProperty]
            public string Key;
            [ModelAssetReference]
            public Texture2D Texture;
        }
    }
}
