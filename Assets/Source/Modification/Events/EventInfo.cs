using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Events
{
    [CreateAssetMenu(fileName = "NewEventInfo", menuName = "BFA2/Modifiers/Event Info")]
    public class EventInfo : ScriptableObject
    {
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
    }
}
