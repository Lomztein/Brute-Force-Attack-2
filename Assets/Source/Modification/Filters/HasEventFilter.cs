using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class HasEventFilter : StructureFilter
    {
        [ModelAssetReference]
        public EventInfo Info;

        public override bool Check(Structure structure) => structure.Events.HasEvent(Info.Identifier);
    }
}
