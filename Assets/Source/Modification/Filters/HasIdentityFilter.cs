using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Filters;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class HasIdentityFilter : IModdableFilter
    {
        [ModelProperty]
        public string Identiifier;

        public bool Check(IModdable moddable)
        {
            if (moddable is IIdentifiable identifiable)
            {
                return identifiable.Identifier == Identiifier;
            }
            return false;
        }
    }
}
