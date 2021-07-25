using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class HasStatFilter : StructureFilter
    {
        [ModelAssetReference]
        public StatInfo Info;

        public override bool Check(Structure structure) => structure.Stats.HasStat(Info.Identifier);
    }
}
