using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class OrFilter : IModdableFilter
    {
        [ModelProperty]
        public IModdableFilter[] Filters;

        public bool Check(IModdable moddable)
        {
            return Filters.Any(x => x.Check(moddable));
        }
    }
}
