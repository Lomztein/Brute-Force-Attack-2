using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class HasTagFilter : IModdableFilter
    {
        [ModelProperty]
        public string[] Tags;

        public bool Check(IModdable moddable)
        {
            if (moddable is ITagged tagged)
            {
                return Tags.All(x => tagged.HasTag(x));
            }
            return false;
        }
    }
}
