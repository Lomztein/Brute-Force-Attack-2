using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Highlighters
{
    [CreateAssetMenu(fileName = "NewHighlighterSet", menuName = "BFA2/Highlighter Set")]
    public class HighlighterSet : ScriptableObject
    {
        [ModelProperty]
        public string[] HighlighterIdentifiers;

        public bool Contains(string identifier) => HighlighterIdentifiers.Contains(identifier);
    }
}
