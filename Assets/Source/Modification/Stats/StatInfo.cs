using Lomztein.BFA2.Modification.Stats.Formatters;
using Lomztein.BFA2.Modification.Stats.Functions;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Modification.Stats
{
    [CreateAssetMenu(fileName = "NewStatInfo", menuName = "BFA2/Modifiers/StatInfo")]
    public class StatInfo : ScriptableObject
    {
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty, SerializeReference, SR]
        public IStatFunction Function;
        [ModelProperty, SerializeReference, SR]
        public IStatFormatter Formatter;
    }
}
